#pragma once

namespace lab1 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Сводка для MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
		}

	protected:
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::TextBox^ textBox1;
	protected:

	private: System::Windows::Forms::TextBox^ textBox2;

	private: System::Windows::Forms::Button^ button2;
	private: System::Windows::Forms::Button^ button3;
	private: System::Windows::Forms::Button^ button4;
	private: System::Windows::Forms::Button^ button5;





	private: System::Windows::Forms::Label^ label1;

	private:
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		void InitializeComponent(void)
		{
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->button3 = (gcnew System::Windows::Forms::Button());
			this->button4 = (gcnew System::Windows::Forms::Button());
			this->button5 = (gcnew System::Windows::Forms::Button());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// textBox1
			// 
			this->textBox1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 18, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->textBox1->Location = System::Drawing::Point(42, 73);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(138, 35);
			this->textBox1->TabIndex = 0;
			this->textBox1->Text = L"0";
			this->textBox1->TextAlign = System::Windows::Forms::HorizontalAlignment::Center;
			this->textBox1->TextChanged += gcnew System::EventHandler(this, &MyForm::textBox1_TextChanged);
			this->textBox1->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::textBox_KeyPress);
			// 
			// textBox2
			// 
			this->textBox2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 18, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->textBox2->Location = System::Drawing::Point(328, 73);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(138, 35);
			this->textBox2->TabIndex = 3;
			this->textBox2->Text = L"0";
			this->textBox2->TextAlign = System::Windows::Forms::HorizontalAlignment::Center;
			this->textBox2->TextChanged += gcnew System::EventHandler(this, &MyForm::textBox3_TextChanged);
			this->textBox2->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::textBox_KeyPress);
			// 
			// button2
			// 
			this->button2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->button2->Location = System::Drawing::Point(211, 123);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(75, 38);
			this->button2->TabIndex = 4;
			this->button2->Text = L"+";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->Click += gcnew System::EventHandler(this, &MyForm::button2_Click);
			// 
			// button3
			// 
			this->button3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->button3->Location = System::Drawing::Point(211, 167);
			this->button3->Name = L"button3";
			this->button3->Size = System::Drawing::Size(75, 41);
			this->button3->TabIndex = 5;
			this->button3->Text = L"-";
			this->button3->UseVisualStyleBackColor = true;
			this->button3->Click += gcnew System::EventHandler(this, &MyForm::button3_Click);
			// 
			// button4
			// 
			this->button4->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->button4->Location = System::Drawing::Point(211, 214);
			this->button4->Name = L"button4";
			this->button4->Size = System::Drawing::Size(75, 41);
			this->button4->TabIndex = 6;
			this->button4->Text = L"*";
			this->button4->UseVisualStyleBackColor = true;
			this->button4->Click += gcnew System::EventHandler(this, &MyForm::button4_Click);
			// 
			// button5
			// 
			this->button5->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 15.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->button5->Location = System::Drawing::Point(211, 261);
			this->button5->Name = L"button5";
			this->button5->Size = System::Drawing::Size(75, 39);
			this->button5->TabIndex = 7;
			this->button5->Text = L"/";
			this->button5->UseVisualStyleBackColor = true;
			this->button5->Click += gcnew System::EventHandler(this, &MyForm::button5_Click);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 24, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(506, 214);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(101, 37);
			this->label1->TabIndex = 13;
			this->label1->Text = L"Sum: ";
			this->label1->Click += gcnew System::EventHandler(this, &MyForm::label1_Click);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(754, 461);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->button5);
			this->Controls->Add(this->button4);
			this->Controls->Add(this->button3);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->textBox1);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			this->Load += gcnew System::EventHandler(this, &MyForm::MyForm_Load);
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void MyForm_Load(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void textBox3_TextChanged(System::Object^ sender, System::EventArgs^ e) {
	}
private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {
	try {
		double num1 = Double::Parse(textBox1->Text);
		double num2 = Double::Parse(textBox2->Text);
		double sum = num1 + num2;
		label1->Text = "Sum: " + sum.ToString();
	}
	catch (FormatException^) {
		label1->Text = "Invalid input!";
	}
}
private: System::Void textBox1_TextChanged(System::Object^ sender, System::EventArgs^ e) {
}
private: System::Void numericUpDown1_ValueChanged(System::Object^ sender, System::EventArgs^ e) {
}
private: System::Void label1_Click(System::Object^ sender, System::EventArgs^ e) {
}

private: System::Void button2_Click(System::Object^ sender, System::EventArgs^ e) {
	double output = System::Convert::ToDouble(textBox1->Text) + System::Convert::ToDouble(textBox2->Text);
	output = Math::Round(output, 4);
	label1->Text = System::Convert::ToString(output);
}

private: System::Void button3_Click(System::Object^ sender, System::EventArgs^ e) {
	double output = System::Convert::ToDouble(textBox1->Text) - System::Convert::ToDouble(textBox2->Text);
	output = Math::Round(output, 4);
	label1->Text = System::Convert::ToString(output);
}

private: System::Void button4_Click(System::Object^ sender, System::EventArgs^ e) {
	double output = System::Convert::ToDouble(textBox1->Text) * System::Convert::ToDouble(textBox2->Text);
	output = Math::Round(output, 4);
	label1->Text = System::Convert::ToString(output);
}

private: System::Void button5_Click(System::Object^ sender, System::EventArgs^ e) {
	double output = System::Convert::ToDouble(textBox1->Text) / System::Convert::ToDouble(textBox2->Text);
	output = Math::Round(output, 4);
	label1->Text = System::Convert::ToString(output);
}

private:
	System::Void textBox_KeyPress(System::Object^ sender, KeyPressEventArgs^ e) {

		TextBox^ currentTextBox = safe_cast<TextBox^>(sender);

		String^ currentText = currentTextBox->Text;
		int cursorPosition = currentTextBox->SelectionStart;

		if (!Char::IsDigit(e->KeyChar)) {
			if (e->KeyChar == '-' && cursorPosition == 0 && !currentText->Contains("-")) {
				e->Handled = false;
			}
			else if (e->KeyChar == ',' && !currentText->Contains(",")) {
				e->Handled = false;
			}
			else if (e->KeyChar == (wchar_t)Keys::Back) {
				e->Handled = false;
			}
			else
			{
				e->Handled = true;
				MessageBox::Show("Not a number or '-' and ','");
			}
		}
		/*if (!Char::IsDigit(e->KeyChar) && e->KeyChar != 0x08) {
			e->Handled = false;
		}*/
		
	}
};
}
