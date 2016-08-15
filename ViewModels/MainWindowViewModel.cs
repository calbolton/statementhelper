using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using StatementHelper.Helpers;
using StatementHelper.Models;

namespace StatementHelper.ViewModels
{
    public class MainWindowViewModel : WorkspaceViewModel
    {
        private IOHelper _ioHelper;

        public MainWindowViewModel()
        {
            Test = "TEST";
            Categories = new ObservableCollection<Category>(new List<Category>()
            {
                new Category() {Id = 1, Name = "Bank Charges"},
                new Category() {Id = 2, Name = "Credit Card"},
                new Category() {Id = 2, Name = "Medical Aid"},
                new Category() {Id = 2, Name = "Car Repayment"},
                new Category() {Id = 2, Name = "Dad"},
                new Category() {Id = 2, Name = "Other"},
                new Category() {Id = 2, Name = "Car Insurance"},
                new Category() {Id = 2, Name = "Car"},
                new Category() {Id = 2, Name = "Food"},
                new Category() {Id = 2, Name = "Data"},
                new Category() {Id = 2, Name = "Airtime"},
                new Category() {Id = 2, Name = "Income"},
                new Category() {Id = 2, Name = "Withdrawals"},
                new Category() {Id = 2, Name = "Investment"},
                new Category() {Id = 2, Name = "Health"},
                new Category() {Id = 3, Name = "Garage Card"}
            }.OrderBy(x => x.Name));

           _ioHelper = new IOHelper(@"C:\Users\calbo_000\Mine\Bank Statements\Test.txt");

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
            StatementItems = new ObservableCollection<StatementItem>(_ioHelper.LoadStatementItems());
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
            set { _categories = value; OnPropertyChanged("Categories");}
        }

        public ObservableCollection<StatementItem> StatementItems
        {
            get { return _statementItems; }
            set { _statementItems = value; OnPropertyChanged("StatementItems");}
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

        private void Save()
        {
            _ioHelper.SaveStatements(StatementItems);
        }

        private void CalculateSummaryItems()
        {
            var groupedItems = _statementItems
                .Where(x => x.Category != null && x.DateTime >= StartDate && x.DateTime <= EndDate)
                .GroupBy(x => x.Category.Name)
                .Select(groupedItem => new SummaryItem()
                {
                    Name = groupedItem.Key,
                    Amount = groupedItem.Sum(x => x.Amount)
                })
                .OrderByDescending(x => x.Amount);

            SummaryItems = new ObservableCollection<SummaryItem>(groupedItems.ToList());
        }

        private void LoadStatement()
        {
            var  processor = new CSVProcessor();

            var path = @"C:\Users\calbo_000\Mine\Bank Statements\Statement_1948146169_110 (1).csv";

            var si = CSVProcessor.ExtractClassList<StatementItem>(path, ',', false);

            MergeStatementItems(si);
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
    }
}
