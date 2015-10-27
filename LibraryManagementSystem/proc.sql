--use LibraryManagementSystemDataBase

CREATE PROC search
@JNO nchar(10), @MYCURSOR CURSOR VARYING OUTPUT
AS
SET @MYCURSOR = CURSOR
FOR
SELECT dboBook.Book_ISBN 图书ISBN, Book_NAME 图书名, Book_MAINAUTHOR 主编, Book_SEARCHID 图书索引号, Book_CLASS 图书类别,Book_PUBLISHER 出版社, Book_PRICE 定价,Book_REMAIN 剩余量 
FROM dboBook, dboSearchRecord
WHERE dboBook.Book_ISBN=dboSearchRecord.SearchRecord_Book_ISBN
OPEN @MYCURSOR
