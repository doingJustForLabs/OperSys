#pragma once

namespace lab1 {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Сводка для MyForm2
	/// </summary>
	public ref class MyForm2 : public System::Windows::Forms::Form
	{
	public:
		MyForm2(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
			pictureBox1->BringToFront();
			pictureBox1->Image = Image::FromFile("myLittleCow.png");

			pictureBox1->SizeMode = PictureBoxSizeMode::Zoom;
			
			pictureBox1->Location = Point(0, 0);

			timer1 = gcnew Timer();
			timer1->Interval = 50;
			timer1->Tick += gcnew EventHandler(this, &MyForm2::timer1_Tick);
			timer1->Start();
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~MyForm2()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::PictureBox^ pictureBox1;
	protected:
	private: System::Windows::Forms::Timer^ timer1;
	private: System::ComponentModel::IContainer^ components;

	private:
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		int stepX = 8;
		int stepY = 8;
		int counter = 0;

	private: System::Windows::Forms::Label^ label1;
		   


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->components = (gcnew System::ComponentModel::Container());
			this->pictureBox1 = (gcnew System::Windows::Forms::PictureBox());
			this->timer1 = (gcnew System::Windows::Forms::Timer(this->components));
			this->label1 = (gcnew System::Windows::Forms::Label());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->pictureBox1))->BeginInit();
			this->SuspendLayout();
			// 
			// pictureBox1
			// 
			this->pictureBox1->BackColor = System::Drawing::Color::Transparent;
			this->pictureBox1->Cursor = System::Windows::Forms::Cursors::Hand;
			this->pictureBox1->Location = System::Drawing::Point(331, 134);
			this->pictureBox1->Name = L"pictureBox1";
			this->pictureBox1->Size = System::Drawing::Size(150, 200);
			this->pictureBox1->TabIndex = 0;
			this->pictureBox1->TabStop = false;
			this->pictureBox1->Click += gcnew System::EventHandler(this, &MyForm2::pictureBox1_Click);
			// 
			// timer1
			// 
			//this->timer1->Tick += gcnew System::EventHandler(this, &MyForm2::timer1_Tick);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 14.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(42, 442);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(222, 24);
			this->label1->TabIndex = 1;
			this->label1->Text = L"Корова остановлена: 0 ";
			this->label1->Click += gcnew System::EventHandler(this, &MyForm2::label1_Click);
			// 
			// MyForm2
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(990, 643);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->pictureBox1);
			this->Name = L"MyForm2";
			this->Text = L"MyForm2";
			this->Load += gcnew System::EventHandler(this, &MyForm2::MyForm2_Load);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->pictureBox1))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void MyForm2_Load(System::Object^ sender, System::EventArgs^ e) {
	}
	private: System::Void pictureBox1_Click(System::Object^ sender, System::EventArgs^ e) {
		if (timer1->Enabled) {
			timer1->Stop();
			counter++;
			label1->Text = "Корова остановлена: " + counter.ToString();
		}
		else
		{
			timer1->Start();
		}
	}
	private: System::Void timer1_Tick(System::Object^ sender, System::EventArgs^ e) {
		int newX = pictureBox1->Location.X + stepX;
		int newY = pictureBox1->Location.Y + stepY;

		if (newX < 0 || newX + pictureBox1->Width > this->ClientSize.Width) {
			stepX *= -1; // Меняем направление по X
		}
		if (newY < 0 || newY + pictureBox1->Height > this->ClientSize.Height) {
			stepY *= -1; // Меняем направление по Y
		}

		pictureBox1->Location = Point(newX, newY);
	}
	private: System::Void label1_Click(System::Object^ sender, System::EventArgs^ e) {
	}
};
}
