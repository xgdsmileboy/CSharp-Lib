--use LibraryManagementSystemDataBase

CREATE PROC search
@JNO nchar(10), @MYCURSOR CURSOR VARYING OUTPUT
AS
SET @MYCURSOR = CURSOR
FOR
SELECT dboBook.Book_ISBN ͼ��ISBN, Book_NAME ͼ����, Book_MAINAUTHOR ����, Book_SEARCHID ͼ��������, Book_CLASS ͼ�����,Book_PUBLISHER ������, Book_PRICE ����,Book_REMAIN ʣ���� 
FROM dboBook, dboSearchRecord
WHERE dboBook.Book_ISBN=dboSearchRecord.SearchRecord_Book_ISBN
OPEN @MYCURSOR
