## Simple Console Accounting App 

Reads staff data from a file and generates payslips accordingly

### Usage

1. Save a list of staff names and positions to a file named staff.txt in the same directory as ConsoleAccounting.exe

   example:
   ```
   // staff.txt

   Alice, Manager
   Bob, Admin
   Charlie, Staff
   ...
   ```

2. Run ConsoleAccounting.exe and it will read from the staff.txt file

3. Enter the appropriate month and year when asked

4. Enter the hours each employee worked this month

### Output

A payslip will be generated for each employee as well as a summary of staff who worked less than 10 hours
Payslips are saved to the same directory under each staff member's name
Summary saved as Summary.txt