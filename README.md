[LibrarySystem.cs]/[AddBook]/Line 27
Bug: Lacking way to validate if the ISBN is numeric or consists of the correct number of digits. 
Resulting in books being added without a valid ISBN-number. 

[LibrarySystem.cs]/[AddBook]/Line 27
Bug: Method allows for duplicate ISBN-numbers. 

[LibrarySystem.cs]/[RemoveBook]/Line 39~
Bug: Book was removed despite being borrowed. 

[LibrarySystem.cs]/[SearchByTitle]/Line 55~
Bug: Incomplete and lowercase titles don't get added to list. 

[LibrarySystem.cs]/[BorrowBook]/Line 65~
Bug: Books that are already borrowed are given a new borrow date. 

[LibrarySystem.cs]/[ReturnBook]/Line 77~
Bug: BorrowDate on books don't get nullified upon return.

[LibrarySystem.cs]/[CalculateLateFee]/Line 94~
Bug: Did not calculate late fee correctly. 
