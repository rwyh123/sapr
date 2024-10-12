using sapr.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sapr.Stores
{
    public class SmthStore
    {
        private static SmthStore instance;

        // Приватное поле для хранения пользовательских данных
        private Point userData;

        // Приватный конструктор
        private SmthStore()
        {
            // Инициализация по умолчанию
            userData = new Point();
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static SmthStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SmthStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(Point Point)
        {
            userData = Point;
        }

        // Метод для получения пользовательских данных
        public Point GetUserData()
        {
            return userData;
        }
    }
}
