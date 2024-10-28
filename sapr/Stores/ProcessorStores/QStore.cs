using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Stores.ProcessorStores
{
    public class QStore
    {
        private static QStore instance;

        // Приватное поле для хранения пользовательских данных
        public List<double> QX;

        // Приватный конструктор
        private QStore()
        {
            // Инициализация по умолчанию
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static QStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(List<double> qx)
        {
            QX = qx;
        }

        // Метод для получения пользовательских данных
        public List<double> GetUserData()
        {
            return QX;
        }
    }
}
