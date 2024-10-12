using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sapr.Stores
{
    public class EPowerStore
    {
        private static EPowerStore instance;

        // Приватное поле для хранения пользовательских данных
        private double userData;

        // Приватный конструктор
        private EPowerStore()
        {
            // Инициализация по умолчанию
            userData = new double();
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static EPowerStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EPowerStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(double Point)
        {
            userData = Point;
        }

        // Метод для получения пользовательских данных
        public double GetUserData()
        {
            return userData;
        }
    }
}
