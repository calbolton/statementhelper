using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using StatementHelper.Helpers;
using StatementHelper.Models;

namespace StatementHelper.ViewModels
{
    public class BudgetSearchViewModel : WorkspaceViewModel
    {
        private IOHelper _ioHelper;

        public BudgetSearchViewModel()
        {
            //Test = "TEST";
            //Categories = new ObservableCollection<Category>(new List<Category>()
            //{
            //    new Category() {Id = 1, Name = "Bank Charges"},
            //    new Category() {Id = 2, Name = "Credit Card"},
            //    new Category() {Id = 2, Name = "Medical Aid"},
            //    new Category() {Id = 2, Name = "Car Repayment"},
            //    new Category() {Id = 2, Name = "Dad"},
            //    new Category() {Id = 2, Name = "Other"},
            //    new Category() {Id = 2, Name = "Car Insurance"},
            //    new Category() {Id = 2, Name = "Car"},
            //    new Category() {Id = 2, Name = "Food"},
            //    new Category() {Id = 2, Name = "Data"},
            //    new Category() {Id = 2, Name = "Airtime"},
            //    new Category() {Id = 2, Name = "Income"},
            //    new Category() {Id = 2, Name = "Withdrawals"},
            //    new Category() {Id = 2, Name = "Investment"},
            //    new Category() {Id = 2, Name = "Health"},
            //    new Category() {Id = 3, Name = "Garage Card"},
            //    new Category() {Id = 3, Name = "House"}
            //}.OrderBy(x => x.Name));

            _ioHelper = new IOHelper(@"C:\Temp\SH_Data.txt");
            
            LoadStatements();
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            CalculateSummaryItems();
        }

        private void CleanItems()
        {
            StatementItems = new ObservableCollection<StatementItem>(StatementItems.Where(x => x.Amount != 0));
            Save();
        }

        private void LoadStatements()
        {
            var accountNumber = "12345";
            var user = new User("8309115022086", "Cal Bolton");
            var account = new Account(accountNumber);
            var budget = CreateBudget();

            user.SetBudget(budget);
            user.AddAccount(account);
            LoadStatement();
            user.AddStatementItems(accountNumber, StatementItems);

            Budgets = user.Budget.BudgetInstances;
        }

        private static Budget CreateBudget()
        {
            DateTime fromDate = new DateTime(2016, 07, 01);
            DateTime toDate = new DateTime(2016, 7, 31);
            decimal income = 30000;
            ICollection<BudgetItem> budgetItems = new List<BudgetItem>();
            var bankCharges = new BudgetItem("Bank Charges", 200, new List<string>()
            {
                "GREENBACKS",
                "MAINTENANCE FEE",
                "NEDLIFE",
                "ATM/SSD FEE"
            });

            var house = new BudgetItem("House", 2000, new List<string>()
            {
                "RIETPAN",
                "PAINT",
                "MAKRO",
                "Furniture",
                "KALAN",
                "GAME",
                "GARDEN SHOP"
            });

            var food = new BudgetItem("Food", 3000, new List<string>()
            {
                "PNP"
            });

            var withdrawals = new BudgetItem("Withdrawals", 1000, new List<string>()
            {
                "ATM CASH",
                "SASW CASH"
            });

            var dataAndAirtime = new BudgetItem("Data & Airtime", 500, new List<string>()
            {
                "Data",
                "Airtime"
            });

            var medicalAid = new BudgetItem("Medical Aid", 2344, new List<string>()
            {
                "DISC PREM"
            });

            var homeloan = new BudgetItem("Homeloan", 13160.17M, new List<string>()
            {
                "FNB H LOAN"
            });

            var carAndHouseInsurance = new BudgetItem("Car & House Insurance", 1021.07M, new List<string>()
            {
                "DISCINSURE"
            });

            var buildingInsurance = new BudgetItem("Building Insurance", 300, new List<string>()
            {
                "OOBA"
            });

            var ekurhuleni = new BudgetItem("Ekurhuleni", 1500, new List<string>()
            {
                "EMMES"
            });

            var internet = new BudgetItem("Internet", 700, new List<string>()
            {
                "axxess"
            });
            var security = new BudgetItem("Security", 595, new List<string>()
            {
                "Security"
            });

            var petrol = new BudgetItem("Petrol", 2500, new List<string>()
            {
                "Telkom"
            });

            var templateItems = new List<BudgetItem>()
            {
                food,
                petrol,
                security,
                ekurhuleni,
                internet,
                buildingInsurance,
                house,
                withdrawals,
                dataAndAirtime,
                medicalAid,
                homeloan,
                carAndHouseInsurance,
                bankCharges
            };

            return new Budget(30000, BudgetPeriod.Monthly, templateItems);
        }

        public BudgetInstance CurrentBudget
        {
            get { return _currentBudget; }
            set { _currentBudget = value; OnPropertyChanged("CurrentBudget"); }
        }

        public ICollection<BudgetInstance> Budgets
        {
            get { return _budgets; }
            set
            {
                _budgets = value;
                OnPropertyChanged("Budgets");
            }
        }

        public ObservableCollection<SummaryItem> SummaryItems
        {
            get { return _summaryItems; }
            set { _summaryItems = value; OnPropertyChanged("SummaryItems"); }
        }

        public string Test { get; set; }

        public StatementItem SelectedStatementItem
        {
            get { return _selectedStatementItem; }
            set { _selectedStatementItem = value; OnPropertyChanged("SelectedStatementItem"); }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; CalculateSummaryItems(); OnPropertyChanged("StartDate"); }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; CalculateSummaryItems(); OnPropertyChanged("EndDate"); }
        }


        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged("Categories"); }
        }

        public ObservableCollection<StatementItem> StatementItems
        {
            get { return _statementItems; }
            set { _statementItems = value; OnPropertyChanged("StatementItems"); }
        }


        private ICommand _loadStatementCommand;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<StatementItem> _statementItems;
        private StatementItem _selectedStatementItem;

        public ICommand LoadStatementCommand
        {
            get
            {
                if (_loadStatementCommand == null)
                {
                    _loadStatementCommand = new RelayCommand(x => LoadStatement());
                }

                return _loadStatementCommand;
            }
        }

        private ICommand _saveCommand;
        private DateTime _endDate;
        private DateTime _startDate;
        private ObservableCollection<SummaryItem> _summaryItems;
        private ICollection<BudgetInstance> _budgets;
        private BudgetInstance _currentBudget;
        private ICommand _viewBudgetCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(x => Save());
                }

                return _saveCommand;
            }
        }

        public ICommand ViewBudgetCommand
        {
            get
            {
                if (_viewBudgetCommand == null)
                {
                    _viewBudgetCommand = new RelayCommand(x => ViewBudget());
                }
                return _viewBudgetCommand;
            }
        }

        private void ViewBudget()
        {
            var e = new ViewBudgetItemClickedEventArgs(CurrentBudget);
            OnViewBudgetClicked(e);
        }

        private void Save()
        {
            _ioHelper.SaveStatements(StatementItems);
        }

        private void CalculateSummaryItems()
        {
            //var groupedItems = _statementItems
            //    .Where(x => x.Category != null && x.DateTime >= StartDate && x.DateTime <= EndDate)
            //    .GroupBy(x => x.Category.Name)
            //    .Select(groupedItem => new SummaryItem()
            //    {
            //        Name = groupedItem.Key,
            //        Amount = groupedItem.Sum(x => x.Amount)
            //    })
            //    .OrderByDescending(x => x.Amount);

            //SummaryItems = new ObservableCollection<SummaryItem>(groupedItems.ToList());
        }

        private void LoadStatement()
        {
            var processor = new CSVProcessor();

            var path = @"C:\Users\Cal\Mine\Statements\Statement_1948146169_111 (1).csv";

            StatementItems = new ObservableCollection<StatementItem>(CSVProcessor.ExtractClassList<StatementItem>(path, ',', false));

           // MergeStatementItems(si);
        }

        private void MergeStatementItems(ICollection<StatementItem> statementItems)
        {
            foreach (var item in statementItems)
            {
                if (!StatementItems.Any(x => x.DateTime == item.DateTime && x.Description == item.Description))
                {
                    StatementItems.Add(item);
                }
            }
        }

        public delegate void ViewBudgetItemClickedEventHandler(object sender, ViewBudgetItemClickedEventArgs e);

        public event ViewBudgetItemClickedEventHandler ViewBudgetClicked;

        protected virtual void OnViewBudgetClicked(ViewBudgetItemClickedEventArgs e)
        {
            ViewBudgetClicked?.Invoke(this, e);
        }
    }
}
