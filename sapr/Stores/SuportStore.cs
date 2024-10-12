using sapr.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Stores
{
    public class SuportStore
    {
        // Приватная статическая переменная, содержащая единственный экземпляр класса
        private static SuportStore instance;

        // Приватное поле для хранения пользовательских данных
        private ObservableCollection<SupportModelv2> userData;

        // Приватный конструктор
        private SuportStore()
        {
            // Инициализация по умолчанию
            userData = new ObservableCollection<SupportModelv2>();
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static SuportStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SuportStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(ObservableCollection<SupportModelv2> user)
        {
            userData = user;
        }

        // Метод для получения пользовательских данных
        public ObservableCollection<SupportModelv2> GetUserData()
        {
            return userData;
        }
    }
}
