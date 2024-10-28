using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Stores.ProcessorStores
{
    public class StepStore
    {
        private static StepStore instance;

        // Приватное поле для хранения пользовательских данных
        public int Step;

        // Приватный конструктор
        private StepStore()
        {
            // Инициализация по умолчанию
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static StepStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StepStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(int step)
        {
            Step = step;
        }

        // Метод для получения пользовательских данных
        public int GetUserData()
        {
            return Step;
        }
    }
}
