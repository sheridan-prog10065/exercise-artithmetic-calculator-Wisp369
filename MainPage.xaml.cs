using System.Collections.ObjectModel;
using Debug = System.Diagnostics.Debug;

namespace MathOperators;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// Remembers the expressions that were calculated by the user
    /// </summary>
    private ObservableCollection<string> _expList;

    public MainPage()
    {
        InitializeComponent();
        _expList = new ObservableCollection<string>();
        _lstExpHistory.ItemsSource = _expList;
    }

    private async void OnCalculate(object sender, EventArgs e)
    {
        try
        {
          //Get the input to the arithmetic operation
            double leftOperand = double.Parse(_txtLeftOp.Text);
            double rightOperand = double.Parse(_txtRightOp.Text);
            char operation = ((string)_pckOperand.SelectedItem)[0];

            //Perform the arithmetic operation and obtain the result
            double result = PerformArithmeticOperation(operation, leftOperand, rightOperand);

            //Display the arithmetic calculation to the user
            string expression = $"{leftOperand} {operation} {rightOperand} = {result}";
            _txtMathExp.Text = expression;
            //Remember the expression
            _expList.Add(expression);
            //Refresh the list view
        }
        catch (ArgumentNullException ex)
        {
            await DisplayAlert("Error", "Please provide the correct input", "OK");
        }
        catch (FormatException ex)
        {
          //The user has provided input but it is incorrect (not a number)
          await DisplayAlert("Error", "Please provide the correct input", "OK");
        }
        catch (DivideByZeroException ex)
        {
            await DisplayAlert("Error", "Please provide a non-zero denominatior!", "Ok");
        }
    }

    private double PerformArithmeticOperation(char operation, double leftOperand, double rightOperand)
    {
        //TODO: Implement the calculation
        switch (operation)
        {
            case '+':
                return PerformAddition(leftOperand, rightOperand);
            
            case '-':
                return PerformSubtraction(leftOperand, rightOperand);
            
            case '*':
                return PerformMultiplication(leftOperand, rightOperand);
            
            case '/':
                return PerformDivision(leftOperand, rightOperand);
            
            case '%':
                return PerformDivRemainder(leftOperand, rightOperand);
            
            default:
                Debug.Assert(false, "Unknown operand, cannot calculate operation");
                return 0;
        }
    }
    private double PerformAddition(double leftOperand, double rightOperand)
    {
        return (leftOperand + rightOperand);
    }
    private double PerformSubtraction(double leftOperand, double rightOperand)
    {
        return (leftOperand - rightOperand);
    }
    private double PerformMultiplication(double leftOperand, double rightOperand)
    {
        return (leftOperand * rightOperand);
    }
    private double PerformDivision(double leftOperand, double rightOperand)
    {
        string divOp = _pckOperand.SelectedItem as string; //Another way of casting used for objects
        if (divOp.Contains("Int", StringComparison.OrdinalIgnoreCase))
        {
            //Integer Division
            int intLeftOp = (int)leftOperand;
            int intRightOp = (int)rightOperand;
            return intLeftOp / intRightOp;
        }
        return (leftOperand / rightOperand);
    }
    private double PerformDivRemainder(double leftOperand, double rightOperand)
    {
        return (leftOperand % rightOperand);
    }
}
