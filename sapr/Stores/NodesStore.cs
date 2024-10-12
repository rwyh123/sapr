using sapr.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sapr.Stores
{
    public class NodesStore
    {
        // Приватная статическая переменная, содержащая единственный экземпляр класса
        private static NodesStore instance;

        // Приватное поле для хранения пользовательских данных
        private ObservableCollection<NodeModel> userData;

        // Приватный конструктор
        private NodesStore()
        {
            // Инициализация по умолчанию
            userData = new ObservableCollection<NodeModel>();
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static NodesStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NodesStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(ObservableCollection<NodeModel> node)
        {
            userData = node;
        }

        // Метод для получения пользовательских данных
        public ObservableCollection<NodeModel> GetUserData()
        {
            return userData;
        }
    }
}
