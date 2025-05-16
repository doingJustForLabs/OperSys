#include "MyForm.h"
#include "MyForm1.h"
#include "MyForm2.h"

using namespace System;

using namespace System::Windows::Forms;

[STAThread]

int main(array<String^>^ argv)

{

    Application::EnableVisualStyles();

    Application::SetCompatibleTextRenderingDefault(false);

    //Application::Run(gcnew lab1::MyForm());

    lab1::MyForm form;
    lab1::MyForm1 form1;
    lab1::MyForm2 form2;

    /*form.Show();
    form1.Show();
    form2.Show();*/

    Application::Run(% form);
    Application::Run(% form1);
    Application::Run(% form2);

    return 0;
}