#pragma once
//#include <fileapi.h>
//#include <windows.h>
//#include <stdlib.h>

namespace lab2 {
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::IO;

	/// <summary>
	/// Сводка для MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		String^ logFilePath = "D:/holn/Desktop/log.txt";
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: добавьте код конструктора
			//
			
			//system("start cmd");
			
			// словарь с логами изменения места
			previousFreeSpace = gcnew System::Collections::Generic::Dictionary<String^, long long>();
			//comboBoxFormat->SelectedItem = "MB";

			String^ logFilePath = "D:/holn/Desktop/log.txt";
			ClearLogFileIfExists(logFilePath);


			timer1 = gcnew System::Windows::Forms::Timer();
			timer1->Interval = 400;
			timer1->Tick += gcnew System::EventHandler(this, &MyForm::timer1_Tick);
			timer1->Start();

			listView1->Columns->Add("Drive", 100);
			listView1->Columns->Add("Type", 100);
			listView1->Columns->Add("Total size", 100);
			listView1->Columns->Add("Total Free Space", 100);
			listView1->Columns->Add("Used Space", 100);

			UpdateListView();

			//for each (String ^ drive in drives)
			//{
			//	Console::WriteLine("Drive: " + drive);

			//	DriveInfo^ driveInfo = gcnew DriveInfo(drive);

			//	// Проверяем, готов ли диск (например, CD/DVD-диск может быть не готов)
			//	if (driveInfo->IsReady)
			//	{
			//		Console::WriteLine("  Drive Type: " + driveInfo->DriveType.ToString());
			//		Console::WriteLine("  Total Size: " + driveInfo->TotalSize.ToString() + " bytes");
			//		Console::WriteLine("  Free Space: " + driveInfo->TotalFreeSpace.ToString() + " bytes");
			//		//Console::WriteLine("  Available space to the current user: " + driveInfo->AvailableFreeSpace.ToString() + " bytes");
			//		
			//	}
			//	else
			//	{
			//		Console::WriteLine("  Drive is not ready.");
			//	}
			//}			
		}

	protected:
		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
			if (timer1)
			{
				delete timer1;
			}
		}

	private: System::Windows::Forms::ListView^ listView1;
	private: System::Windows::Forms::ComboBox^ comboBoxFormat;
	private: System::Windows::Forms::Timer^ timer1;
	private: System::ComponentModel::IContainer^ components;


	protected:

	private:
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		System::Collections::Generic::Dictionary<String^, long long>^ previousFreeSpace;
		//String^ logFilePath;


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		void InitializeComponent(void)
		{
			this->listView1 = (gcnew System::Windows::Forms::ListView());
			this->comboBoxFormat = (gcnew System::Windows::Forms::ComboBox());
			this->SuspendLayout();
			// 
			// listView1
			// 
			this->listView1->HideSelection = false;
			this->listView1->Location = System::Drawing::Point(58, 72);
			this->listView1->Name = L"listView1";
			this->listView1->Size = System::Drawing::Size(675, 286);
			this->listView1->TabIndex = 1;
			this->listView1->UseCompatibleStateImageBehavior = false;
			this->listView1->View = System::Windows::Forms::View::Details;
			this->listView1->SelectedIndexChanged += gcnew System::EventHandler(this, &MyForm::listView1_SelectedIndexChanged);
			// 
			// comboBoxFormat
			// 
			this->comboBoxFormat->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->comboBoxFormat->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"GB", L"MB" });
			this->comboBoxFormat->Location = System::Drawing::Point(316, 403);
			this->comboBoxFormat->Name = L"comboBoxFormat";
			this->comboBoxFormat->Size = System::Drawing::Size(121, 21);
			this->comboBoxFormat->TabIndex = 1;
			this->comboBoxFormat->SelectedIndexChanged += gcnew System::EventHandler(this, &MyForm::comboBoxFormat_SelectedIndexChanged);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(834, 526);
			this->Controls->Add(this->listView1);
			this->Controls->Add(this->comboBoxFormat);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			this->Load += gcnew System::EventHandler(this, &MyForm::MyForm_Load);
			this->ResumeLayout(false);

		}
#pragma endregion
	private: System::Void comboBoxFormat_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e) {
		{
			if (comboBoxFormat->SelectedItem == nullptr)
			{
				//currentFormat = comboBoxFormat->SelectedItem->ToString();
				comboBoxFormat->SelectedItem = "MB";
			}

			// Обновляем ListView с новым форматом
			UpdateListView();
		}
	}

	private: System::Void UpdateListView() {
			listView1->Items->Clear();
			if (comboBoxFormat->SelectedItem == nullptr)
			{
				// Устанавливаем значение по умолчанию
				comboBoxFormat->SelectedItem = "MB";
			}
			String^ format = comboBoxFormat->SelectedItem->ToString();

			array<String^>^ drives = Directory::GetLogicalDrives();

			/*for (int i = 0; i < drives->Length; i++) {
				listView1->Items->Add(drives[i]);
			}*/		

			for each (String ^ drive in drives) {
				DriveInfo^ driveInfo = gcnew DriveInfo(drive);
				//ListViewItem^ item = gcnew ListViewItem(drive);
				
				ListViewItem^ item = gcnew ListViewItem();
				item->Text = drive;

				item->SubItems->Add(driveInfo->DriveType.ToString());

				if (driveInfo->IsReady) {
					item->SubItems->Add(FormatSize(driveInfo->TotalSize, format));
					item->SubItems->Add(FormatSize(driveInfo->TotalFreeSpace, format));

					long long usedSpace = driveInfo->TotalSize - driveInfo->TotalFreeSpace;
					item->SubItems->Add(FormatSize(usedSpace, format));

					LogFreeSpaceChanges(drive, driveInfo->TotalFreeSpace);
					//item->SubItems->Add(String::Format("{0:F2} GB", driveInfo->TotalSize / (1024 * 1024 * 1024)));
				}
				else {
					item->SubItems->Add("N/A");
					item->SubItems->Add("N/A");
					item->SubItems->Add("N/A");
				}

				listView1->Items->Add(item);
				
			}
	}

	private: String^ FormatSize(long long sizeInBytes, String^ format)
	{
		double size = 0;
		if (format == "GB")
		{
			size = sizeInBytes / (1024.0 * 1024.0 * 1024.0); // В гигабайтах
		}
		else if (format == "MB")
		{
			size = sizeInBytes / (1024.0 * 1024.0); // В мегабайтах
		}
		return String::Format("{0:F3} {1}", size, format);
	}

	private: System::Void MyForm_Load(System::Object^ sender, System::EventArgs^ e) {
	}

	private: System::Void listView1_SelectedIndexChanged(System::Object^ sender, System::EventArgs^ e) {
	}

	/*private: System::Void comboBoxFormat(System::Object^ sender, System::EventArgs^ e) {

	}*/
	private: System::Void timer1_Tick(System::Object^ sender, System::EventArgs^ e) {
		//String^ format = comboBoxFormat->SelectedItem != nullptr ? comboBoxFormat->SelectedItem->ToString(): "MB";
		UpdateListView();
	}
	
	private: void LogFreeSpaceChanges(String^ drive, long long currentFreeSpace) {
		if (previousFreeSpace->ContainsKey(drive)) {
			long long previousSpace = previousFreeSpace[drive];
			long long difference = currentFreeSpace - previousSpace; //изменение места на диске
			//long long differenceMB = difference / (1024 * 1024);
			if (difference != 0) {
				String^ logMessage;
				if (difference > 0) {
					logMessage = String::Format("[{0} Диск {1}]: свободное место увеличилось на {2} байт", 
									DateTime::Now.ToString("yyyy-MM-dd HH:mm:ss"),
									drive,
									difference);
					String^ logMessage = String::Format("Диск {0}: свободное место увеличилось на {1} байт", drive, Math::Abs(difference));
					Console::WriteLine(logMessage);
				}
				else {
					logMessage = String::Format("[{0} Диск {1}]: свободное место уменьшилось на {2} байт",
						DateTime::Now.ToString("yyyy-MM-dd HH:mm:ss"),
						drive,
						Math::Abs(difference));
					String^ logMessage = String::Format("Диск {0}: свободное место уменьшилось на {1} байт", drive, Math::Abs(difference));
					Console::WriteLine(logMessage);
				}
				SaveLogToFile(logMessage);
			}
		}
		// Обновляем предыдущее значение
		previousFreeSpace[drive] = currentFreeSpace;
	}
	//сохраняет логи
	private: void SaveLogToFile(String^ message) {
		try {			
			array<String^>^ lines = File::ReadAllLines(logFilePath);

			int maxLines = 100;
			//очистить файл, когда кол-во строчек достигнет maxLines
			if (lines->Length >= maxLines) {
				File::WriteAllText(logFilePath, String::Empty); // Очищаем файл
			}

			StreamWriter^ writer = gcnew StreamWriter(logFilePath, true);
			writer->WriteLine(message);
			writer->Close();
		}
		catch (Exception^ ex) {
			Console::WriteLine("Ошибка при записи лога: " + ex->Message);
		}
	}

	//чистка логов при каждом запуске
	private: 
		void ClearLogFileIfExists(String^ filePath) {
			try {
				if (File::Exists(filePath)) {
					File::WriteAllText(filePath, String::Empty); // Очищаем файл
				}
			}
			catch (Exception^ ex) {
				Console::WriteLine("Ошибка при очистке файла: " + ex->Message);
			}
		}

};
}
