using CustomConsoleATM.Centers;
using CustomConsoleATM.Services;

namespace CustomConsoleATM
{
    public static class ATM
    {
        public static void Withdraw()
        {
            bool isError = false;
            var _transactionCenter = new ATMTransactionCenter();

            Console.WriteLine($"Your balance: ${_transactionCenter.Balance}");

            Console.WriteLine();
            Console.Write("Please enter the withdrawal amount: ");

            _transactionCenter.WithdrawalAmount = Convert.ToInt32(Console.ReadLine());

            if (_transactionCenter.WithdrawalAmount > 0 && _transactionCenter.Balance >= _transactionCenter.WithdrawalAmount)
            {
                try
                {
                    _transactionCenter.BanknotesAmountsToWithdraw =
                        ATMCalculator.GetBanknotesAmountsToWithdraw(
                            _transactionCenter.BanknotesAmountsInATM,
                            _transactionCenter.WithdrawalAmount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    _transactionCenter.WithdrawalAmount = 0;
                    isError = true;
                }
            }
            else
            {
                Console.WriteLine("Entered withdrawal amount is incorrect! Insufficient funds in the account");
                isError = true;
            }

            if (!isError)
                _transactionCenter.Withdraw();

            string status = isError ? "error" : "success";

            Console.WriteLine();
            Console.WriteLine($"Withdrawal status is {status}!");

            Console.ReadKey();
        }
    }
}
