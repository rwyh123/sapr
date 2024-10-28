using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace sapr.Stores
{
    public class NXStore
    {
        private static NXStore instance;

        // Приватное поле для хранения пользовательских данных
        public Dictionary<String, double> NX;

        // Приватный конструктор
        private NXStore()
        {
            // Инициализация по умолчанию
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static NXStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NXStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(Dictionary<String, double> nx)
        {
            NX = nx;
        }

        // Метод для получения пользовательских данных
        public Dictionary<String, double> GetUserData()
        {
            return NX;
        }
    }
}
