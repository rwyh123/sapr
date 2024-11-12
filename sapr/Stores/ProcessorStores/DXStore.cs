using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Stores.ProcessorStores
{
    public class DXStore
    {
        private static DXStore instance;

        // Приватное поле для хранения пользовательских данных
        public Dictionary<String, double> DX;

        // Приватный конструктор
        private DXStore()
        {
            // Инициализация по умолчанию
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static DXStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DXStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(Dictionary<String, double> dx)
        {
            DX = dx;
        }

        // Метод для получения пользовательских данных
        public Dictionary<String, double> GetUserData()
        {
            return DX;
        }
    }
}
