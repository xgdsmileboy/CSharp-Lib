
use LibraryManagementSystemDataBase

---ͼ���
CREATE TABLE dboBook
(
    Book_ISBN char(20) not null,  --ͼ���ISBN��
	Book_NAME char(25) not null,  --����
	Book_AUTHOR char(20) not null,   --����
	Book_SEARCHID char(20) not null,  --��ͼ��ݵı��
	Book_CLASS char(15),  --ͼ���������
	Book_TOTAL smallint not null,   --������
	Book_REMAIN smallint not null,  --ͼ�������ʣ����
	primary key (Book_ISBN) --����
)

--ѧ����Ϣ��
CREATE TABLE dboStudent
(
    Student_IID char(20) not null, --ѧ��
	Student_NAME char(10) unique,  --����
	Student_SEX char(3) check(Student_SEX in ('��','Ů')) not null ,  --�Ա�
	Student_BIRTHDAY date, --����
	Student_dept char(15) not null, --ѧԺ
	Student_PASSWORD char(20), --��¼����
	Student_REGISTRED bit, --��¼ѧ���Ƿ�ע����Ϣ(1:��ע�᣻0��δע��)
	primary key (Student_IID) 
)

--����Ա��Ϣ��
CREATE TABLE dboSys
(
    Sys_IID char(20) not null, --ѧ��
    Sys_NAME char(10) unique,  --����
	Sys_SEX char(3) check(Sys_SEX in ('��','Ů')) not null ,  --�Ա�
	Sys_BIRTHDAY date, --����
	Sys_PASSWORD char(20), --��¼����
	primary key (Sys_IID) 
)

--������Ϣ��
CREATE TABLE dboBorrow
(
    Borrow_user_IID char(20),  --����ѧ��ѧ��
	Borrow_Book_ISBN char(20),  --������ISBN
	Borrow_BORROW_TIME date not null,  --��������
	Borrow_RETURN_TIME date,  --Ӧ�û�������
	Borrow_RE_BORROW smallint,  --�������
	Borrow_BORROW_STATE bit,  --��¼����״̬��1���ѹ黹��0�����ڽ��ģ�
	primary key (Borrow_user_IID, Borrow_Book_ISBN,Borrow_BORROW_TIME),
	foreign key (Borrow_user_IID) references dboStudent(Student_IID), --����
	foreign key (Borrow_Book_ISBN) references dboBook(Book_ISBN)  --����

)

--��ѯ��¼��
CREATE TABLE dboSearchRecord
(
    SearchRecord_user_IID char(20),  --��ѯѧ��
	SearchRecord_Book_ISBN char(20),  --��ѯ��ISBN
	SearchRecord_COUNT int not null,    --���鱻��ѯ�Ĵ���
	primary key (SearchRecord_user_IID, SearchRecord_Book_ISBN),
	foreign key (SearchRecord_user_IID) references dboStudent(Student_IID), --����
	foreign key (SearchRecord_Book_ISBN) references dboBook(Book_ISBN)  --����
)