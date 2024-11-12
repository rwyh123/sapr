using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace sapr.Stores.ProcessorStores
{
    public class ReportTableStore
    {
        private static ReportTableStore instance;

        // Приватное поле для хранения пользовательских данных
        public List<DataGrid> ReportTable;

        // Приватный конструктор
        private ReportTableStore()
        {
            // Инициализация по умолчанию
        }

        // Публичный статический метод для доступа к единственному экземпляру
        public static ReportTableStore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportTableStore();
                }
                return instance;
            }
        }

        // Метод для установки пользовательских данных
        public void SetUserData(List<DataGrid> reportTable)
        {
            ReportTable = reportTable;
        }

        // Метод для получения пользовательских данных
        public List<DataGrid> GetUserData()
        {
            return ReportTable;
        }
    }
}
