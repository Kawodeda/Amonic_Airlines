using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AMONIC_Desktop
{
    public class Authorization
    {
        public static bool IsAuthorized { get; private set; } = false;
        public static Users CurrentUser { get; private set; }
        public static Session CurrentSession { get; private set; }

        public static LoginStatus LogIn(string username, string password)
        {
            try
            {
                var users = DbContextProvider.Context.Users.ToList();
                var user = users.Find(x => x.Email == username);

                if(user != null)
                {
                    if (user.Password == PasswordConverter.Encrypt(password))
                    {
                        if(user.Active == true)
                        {
                            CurrentUser = user;
                            IsAuthorized = true;

                            Session session = new Session();
                            session.StartTime = DateTime.Now;
                            session.Users = CurrentUser;
                            CurrentSession = session;

                            DbContextProvider.Context.Session.Add(CurrentSession);
                            DbContextProvider.Context.SaveChanges();

                            CheckForCrash();

                            return LoginStatus.Success;
                        }
                        else
                        {
                            SendBlockedAttempt(user);

                            return LoginStatus.Blocked;
                        }
                    }
                    else
                    {
                        SendFailedAttempt(user);

                        return LoginStatus.IncorrectInput;
                    }
                }
                else
                {
                    return LoginStatus.IncorrectInput;
                }
            }
            catch
            {
                return LoginStatus.Error;
            }
        }

        public static void LogOut()
        {
            if(IsAuthorized)
            {
                IsAuthorized = false;

                SessionEnd sessionEnd = new SessionEnd();
                sessionEnd.SessionEndStatusId = 1;
                sessionEnd.Time = DateTime.Now;

                CurrentSession.SessionEnd = sessionEnd;

                DbContextProvider.Context.SessionEnd.Add(sessionEnd);
                DbContextProvider.Context.SaveChanges();
            }
        }

        private static void CheckForCrash()
        {
            var sessions = DbContextProvider.Context.Session.ToList().FindAll(x => x.Users == CurrentUser && x != CurrentSession);
            Session lastSession = sessions.Last();

            if(lastSession.SessionEnd == null)
            {
                switch(new CrashReportWindow(lastSession).ShowDialog())
                {
                    case true:
                        MessageBox.Show("Отчет об ошибке успешно отправлен", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    case null:
                        MessageBox.Show("Не удалось отправить отчет об ошибке", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
        }

        private static void SendBlockedAttempt(Users user)
        {
            SendAttempt(user, 3);
        }

        private static void SendFailedAttempt(Users user)
        {
            SendAttempt(user, 2);
        }

        private static void SendAttempt(Users user, int statusId)
        {
            Session session = new Session();
            session.StartTime = DateTime.Now;
            session.Users = user;

            SessionEnd sessionEnd = new SessionEnd();
            sessionEnd.SessionEndStatusId = statusId;
            sessionEnd.Time = DateTime.Now;

            session.SessionEnd = sessionEnd;

            DbContextProvider.Context.SessionEnd.Add(sessionEnd);
            DbContextProvider.Context.Session.Add(session);
            DbContextProvider.Context.SaveChanges();
        }
    }

    public enum LoginStatus
    {
        Success,
        IncorrectInput,
        Error,
        Blocked
    }
}
