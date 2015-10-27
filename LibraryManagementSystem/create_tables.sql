
use LibraryManagementSystemDataBase

---图书表
CREATE TABLE dboBook
(
    Book_ISBN char(20) not null,  --图书的ISBN号
	Book_NAME char(25) not null,  --书名
	Book_AUTHOR char(20) not null,   --作者
	Book_SEARCHID char(20) not null,  --在图书馆的编号
	Book_CLASS char(15),  --图书所属类别
	Book_TOTAL smallint not null,   --书总量
	Book_REMAIN smallint not null,  --图书馆现在剩余量
	primary key (Book_ISBN) --主码
)

--学生信息表
CREATE TABLE dboStudent
(
    Student_IID char(20) not null, --学号
	Student_NAME char(10) unique,  --姓名
	Student_SEX char(3) check(Student_SEX in ('男','女')) not null ,  --性别
	Student_BIRTHDAY date, --生日
	Student_dept char(15) not null, --学院
	Student_PASSWORD char(20), --登录密码
	Student_REGISTRED bit, --记录学生是否注册信息(1:已注册；0：未注册)
	primary key (Student_IID) 
)

--管理员信息表
CREATE TABLE dboSys
(
    Sys_IID char(20) not null, --学号
    Sys_NAME char(10) unique,  --姓名
	Sys_SEX char(3) check(Sys_SEX in ('男','女')) not null ,  --性别
	Sys_BIRTHDAY date, --生日
	Sys_PASSWORD char(20), --登录密码
	primary key (Sys_IID) 
)

--借书信息表
CREATE TABLE dboBorrow
(
    Borrow_user_IID char(20),  --借书学生学号
	Borrow_Book_ISBN char(20),  --所借书ISBN
	Borrow_BORROW_TIME date not null,  --借书日期
	Borrow_RETURN_TIME date,  --应该还书日期
	Borrow_RE_BORROW smallint,  --续借次数
	Borrow_BORROW_STATE bit,  --记录借书状态（1：已归还；0：正在借阅）
	primary key (Borrow_user_IID, Borrow_Book_ISBN,Borrow_BORROW_TIME),
	foreign key (Borrow_user_IID) references dboStudent(Student_IID), --外码
	foreign key (Borrow_Book_ISBN) references dboBook(Book_ISBN)  --外码

)

--查询记录表
CREATE TABLE dboSearchRecord
(
    SearchRecord_user_IID char(20),  --查询学号
	SearchRecord_Book_ISBN char(20),  --查询书ISBN
	SearchRecord_COUNT int not null,    --该书被查询的次数
	primary key (SearchRecord_user_IID, SearchRecord_Book_ISBN),
	foreign key (SearchRecord_user_IID) references dboStudent(Student_IID), --外码
	foreign key (SearchRecord_Book_ISBN) references dboBook(Book_ISBN)  --外码
)