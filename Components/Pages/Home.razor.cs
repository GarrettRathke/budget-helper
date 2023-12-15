using Budget.Data;
using Microsoft.EntityFrameworkCore;

namespace Home
{
    public partial class Home
    {
        public bool ShowCreate { get; set; }
        public bool ShowEdit { get; set; }
        public int EditingId { get; set; }

        private BudgetDataContext? _context;
        public Expense? NewExpense { get; set; }
        public Expense? ExpenseToUpdate { get; set; }
        public List<Expense>? Expenses { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ShowCreate = false;
            await ShowExpenses();
        }

        //------------------ Create! ----------------///
        public void ShowCreateForm()
        {
            NewExpense = new Expense();
            ShowCreate = true;
        }

        public async Task CreateNewExpense()
        {
            _context ??= await BudgetDataContextFactory.CreateDbContextAsync();

            if (NewExpense is not null)
            {
                _context?.Expenses.Add(NewExpense);
                _context?.SaveChangesAsync();
            }

            ShowCreate = false;
            await ShowExpenses();
        }

        //------------------ Read! ----------------///

        public async Task ShowExpenses()
        {
            _context ??= await BudgetDataContextFactory.CreateDbContextAsync();

            if (_context is not null)
            {
                Expenses = await _context.Expenses.ToListAsync();
            }
        }

        //------------------ Update! ----------------///

        public async Task ShowEditForm(Expense expense)
        {

            _context ??= await BudgetDataContextFactory.CreateDbContextAsync();

            if (_context is not null)
            {
                ExpenseToUpdate = _context.Expenses.FirstOrDefault(x => x.Id == expense.Id);
                ShowEdit = true;
                EditingId = expense.Id;
            }
        }

        public async Task UpdateExpense()
        {
            _context ??= await BudgetDataContextFactory.CreateDbContextAsync();

            if (_context is not null)
            {
                if (ExpenseToUpdate is not null) _context.Expenses.Update(ExpenseToUpdate);
                await _context.SaveChangesAsync();
            }
            ShowEdit = false;
        }

        //------------------ Delete! ----------------///

        public async Task DeleteExpense(Expense expense)
        {
            _context ??= await BudgetDataContextFactory.CreateDbContextAsync();
            if (_context is not null)
            {
                if (expense is not null) _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
            await ShowExpenses();
        }
    }
}