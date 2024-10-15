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
        private bool LeftSup;
        private bool RightSup;

        // Приватный конструктор
        private SmthStore()
        {
            // Инициализация по умолчанию
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
        public void SetUserData(bool left,bool right)
        {
            LeftSup = left;
            RightSup = right;
        }

        // Метод для получения пользовательских данных
        public Point GetUserData()
        {
            Point point = new Point(0,0);
            if (LeftSup)
                point.X = 1;
            if (RightSup)
                point.Y = 1;
            return point;
        }
    }
}
