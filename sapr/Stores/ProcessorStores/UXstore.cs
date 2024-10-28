using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Stores
{
    public class UXStore
    {
        private static UXStore instance;

        // Приватное поле для хранения пользовательских данных
        public Dictionary<String, double> UX;

        // Приватный конструктор
        private UXStore()
        {
            // Инициализация по умолчанию
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static UXStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UXStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(Dictionary<String, double> ux)
        {
            UX = ux;
        }

        // Метод для получения пользовательских данных
        public Dictionary<String, double> GetUserData()
        {
            return UX;
        }
    }
}
