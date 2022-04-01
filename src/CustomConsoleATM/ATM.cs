using CustomConsoleATM.Centers;

namespace CustomConsoleATM
{
    public class ATM
    {
        private ATMTransactionCenter TransactionCenter { get; set; }

        public ATM()
        {
            var transactionCenter = new ATMTransactionCenter();
            TransactionCenter = transactionCenter;
        }

        public void Withdraw()
        {
            Console.WriteLine($"Your balance: ${TransactionCenter.Balance}");

            Console.WriteLine();
            Console.Write("Please enter the withdrawal amount: ");

            TransactionCenter.Amount = Convert.ToInt32(Console.ReadLine());

            if (!TransactionCenter.IsError)
                TransactionCenter.Withdraw();

            var status = TransactionCenter.IsError ? "error" : "success";

            Console.WriteLine();
            Console.WriteLine($"Withdrawal status is {status}!");

            Console.ReadKey();
        }
    }
}
