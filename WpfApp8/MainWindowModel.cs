using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfApp8;

public class MainWindowModel : INotifyPropertyChanged
{
    #region MyRegion

    private Guid? _id;
    public Guid? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }
    
    private string? _login;
    public string? Login
    {
        get => _login;
        set => SetField(ref _login, value);
    }
    
    private string? _password;
    public string? Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }
    
    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
        get => _selectedAccount;
        set
        {
            var result = SetField(ref _selectedAccount, value);
            if(!result) return;
            
            Id = _selectedAccount?.Id;
            Login = _selectedAccount?.Login;
            Password = _selectedAccount?.Password;
        }
    }

    public BindingList<Account> Accounts { get; set; } = [];

    #endregion
    
    #region Commands

    public LambdaCommand CommandSave { get; }
    public LambdaCommand CommandDelete { get; }
    public LambdaCommand CommandClear { get; }
    
    #endregion

    public MainWindowModel()
    {
        CommandSave = new LambdaCommand(_ =>
        {
            if (SelectedAccount is null)
            {
                var newAccount = new Account()
                {
                    Id = Guid.NewGuid(),
                    Login = Login!,
                    Password = Password!
                };
                var validationContext = new ValidationContext(newAccount);
                var validationErrors = new List<ValidationResult>();
                var validationResult = Validator.TryValidateObject(newAccount, validationContext, validationErrors, true);
                if (!validationResult)
                {
                    foreach (var validationError in validationErrors)
                    {
                        MessageBox.Show(validationError.ErrorMessage);
                    }
                }
                else
                {
                    Accounts.Add(newAccount);
                }
            }
        });
    }

    #region INotifyPropertyChanged Members
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    #endregion
}