# EasierWorship.Bible

Provides bible data to your app

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [Visual Studio](https://www.visualstudio.com/)
- [Bibledata by liudongmiao](https://github.com/liudongmiao/bibledata)
	- Series of SQLite databases can be found here. Thanks to this guy.

## People to blame

The following personnel is/are responsible for managing this project.

- [actchua@periapsys.com](mailto:actchua@periapsys.com)

### Technology Used

- C#
- [.Net Framwork 4.5.1](#)
- [Dapper](http://dapper-tutorial.net/dapper)
- [System.Data.SQLite](https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki)

### Solution Structure

- EasierWorship.Bible.Data
	- The Business Layer of the system
- PERI.Agenda.Test
    - Unit test project
	
### Sample Code

```csharp

    // Define the location of the bible database
	var bible = new Data.Bible("niv2011.sqlite3");

	// Gets an specific verse
	var res = bible.GetVerse(Data.Bible.Book.Psalms, 119, 10);
	
	// Gets range of verses
	var res1 = bible.GetVerses(Data.Bible.Book.Psalms, 119, 10, 100);
```

### Unit Test

You can test the business logics in the project,```EasierWorship.Bible.Test```. A good knowledge in performing unit testing is required.
