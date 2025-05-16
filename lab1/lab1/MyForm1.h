#pragma once

namespace lab1 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	public ref class MyForm1 : public System::Windows::Forms::Form
	{
	public:
		MyForm1(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//			
			
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~MyForm1()
		{
			if (components)
			{
				delete components;
			}
		}

	private: System::Windows::Forms::Button^ button1;
	protected:
	private: System::Windows::Forms::Button^ button2;

	private:
		/// Обязательная переменная конструктора.
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->button2 = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// button1
			// 
			this->button1->Cursor = System::Windows::Forms::Cursors::PanEast;
			this->button1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 27.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->button1->Location = System::Drawing::Point(102, 102);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(229, 230);
			this->button1->TabIndex = 0;
			this->button1->Text = L"Ушел";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->MouseEnter += gcnew System::EventHandler(this, &MyForm1::button1_MouseEnter);
			this->button1->MouseLeave += gcnew System::EventHandler(this, &MyForm1::button1_MouseLeave);
			// 
			// button2
			// 
			this->button2->Cursor = System::Windows::Forms::Cursors::PanWest;
			this->button2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 27.75F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->button2->Location = System::Drawing::Point(472, 102);
			this->button2->Name = L"button2";
			this->button2->Size = System::Drawing::Size(229, 230);
			this->button2->TabIndex = 1;
			this->button2->Text = L"Ушел";
			this->button2->UseVisualStyleBackColor = true;
			this->button2->MouseEnter += gcnew System::EventHandler(this, &MyForm1::button2_MouseEnter);
			this->button2->MouseLeave += gcnew System::EventHandler(this, &MyForm1::button2_MouseLeave);
			// 
			// MyForm1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(806, 492);
			this->Controls->Add(this->button2);
			this->Controls->Add(this->button1);
			this->Name = L"MyForm1";
			this->Text = L"MyForm1";
			this->Load += gcnew System::EventHandler(this, &MyForm1::MyForm1_Load);
			this->ResumeLayout(false);

		}
#pragma endregion
	private: System::Void MyForm1_Load(System::Object^ sender, System::EventArgs^ e) {
	}


	//private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {
	//}


	private: System::Void button1_MouseEnter(System::Object^ sender, System::EventArgs^ e) {
		button1->Text = "Пришёл";
	}

	
	private: System::Void button1_MouseLeave(System::Object^ sender, System::EventArgs^ e) {
		button1->Text = "Ушёл";
	}


	/*private: System::Void button2_Click(System::Object^ sender, System::EventArgs^ e) {
	}*/

	private: System::Void button2_MouseEnter(System::Object^ sender, System::EventArgs^ e) {
		button2->Text = "Пришёл";
	}


	private: System::Void button2_MouseLeave(System::Object^ sender, System::EventArgs^ e) {
		button2->Text = "Ушёл";
	}

	};
}
