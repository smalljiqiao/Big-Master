/*****
 ***** �������ݿ�ű�
 *****
 ***** ע�⣺
 *****      1.��Ҫ����ʹ��GO���.
 *****      2.�����Ŀ¼D:\DataBase\SQL2017\BigMaster.
 *****      3.���ֶ�ȷ�����������ģ�����Ϊvarchar�����ֶ�ȷ�����ȣ�����Ϊchar���������������ֶ�ȫ������Ϊnvarchar
 *****
 ***** Author��MatueXL
 ***** UpdateTime��2018/07/09
******/

SET STATISTICS TIME OFF

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'BigMaster')
   BEGIN
     RAISERROR('BigMaster���ݿ��Ѵ��ڣ��������ȷ�����ֶ�ɾ�������ݿ⻹�ǽ����޸Ĳ�����',16,1);
     RETURN
   END

ELSE

--����BigMaster���ݿ�

CREATE DATABASE BigMaster

ON PRIMARY  --�����������ļ�
(
  NAME = 'BigMaster',  --�������ļ��߼�����
  FILENAME = 'D:\DataBase\SQL2017\BigMaster\BigMaster.mdf',  --�������ļ�����·��
  SIZE = 10MB,  --���ļ���ʼ��С
  FILEGROWTH = 20%  --�ļ���10%����
)

LOG ON  --������־�ļ�
(
  NAME = 'BigMaster_log',  --��־�ļ��߼�����
  FILENAME = 'D:\DataBase\SQL2017\BigMaster\BigMaster_log.ldf',  --��־�ļ�����·��
  SIZE = 5MB,  --��־�ļ���ʼ��С
  FILEGROWTH = 5MB  --����
)
GO

USE BigMaster;
DECLARE @CurrentUser sysname;  --��Ҳ����������ɶ�����ڱ༭��˵�����ֶ�˵���С�
SELECT @CurrentUser = USER_NAME();

/*
 * �û���
 * ΢����Ȩ��¼Ҳ����������������ݣ���ʱֻд���û�Ψһ��ʶ��Ϣ����������Ȩ�û����ֻ���������д��������Ϣ��
*/
CREATE TABLE Users
(
  ID uniqueidentifier PRIMARY KEY,  --�û�Ψһ��ʶ������
  Phone varchar(50) NULL,  --�ֻ�����
  NickName nvarchar(50) NULL,  --�ǳƣ��ڸ������Ĵ��༭
  Email varchar(50) NULL,  --���䣬�ڸ������Ĵ��༭
  Password varchar(50) NULL,  --��¼����
  Salt char(6) NULL,  --��ֵ
  SaltPassword char(40) NULL, --���ܺ�����
  RegisterTime datetime NULL,  --�״�ע��ʱ��
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --�״���Ȩ��ע��ʱ�䣬�������ݿ⵱ʱ��ʱ��
)


--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', '�û���<br/>΢����Ȩ��¼Ҳ����������������ݣ���ʱֻд���û�Ψһ��ʶ��Ϣ����������Ȩ�û����ֻ���������д��������Ϣ��', 'user',@CurrentUser, 'table', 'Users'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'Users', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '�ֻ�����', 'user', @CurrentUser, 'table', 'Users', 'column', 'Phone'
EXECUTE sp_addextendedproperty 'MS_Description', '�ǳƣ��ڸ������Ĵ��༭', 'user', @CurrentUser, 'table', 'Users', 'column', 'NickName'
EXECUTE sp_addextendedproperty 'MS_Description', '���䣬�ڸ������Ĵ��༭', 'user', @CurrentUser, 'table', 'Users', 'column', 'Email'
EXECUTE sp_addextendedproperty 'MS_Description', '��¼����', 'user', @CurrentUser, 'table', 'Users', 'column', 'Password'
EXECUTE sp_addextendedproperty 'MS_Description', '��ֵ', 'user', @CurrentUser, 'table', 'Users', 'column', 'Salt'
EXECUTE sp_addextendedproperty 'MS_Description', '���ܺ�����', 'user', @CurrentUser, 'table', 'Users', 'column', 'SaltPassword'
EXECUTE sp_addextendedproperty 'MS_Description', '�״�ע��ʱ��', 'user', @CurrentUser, 'table', 'Users', 'column', 'RegisterTime'
EXECUTE sp_addextendedproperty 'MS_Description', '�״���Ȩ��ע��ʱ�䣬�������ݿ⵱ʱ��ʱ��', 'user', @CurrentUser, 'table', 'Users', 'column', 'CreateTime'


/*
 * �û�΢����Ϣ��
 * ���û�ͬ��΢����Ȩ��¼ʱ��ȡ
*/
CREATE TABLE WXUserInfo
(
  ID uniqueidentifier PRIMARY KEY,  --�û�Ψһ��ʶ������
  Openid varchar(50) NULL,  --�û�Openid
  NickName nvarchar(50) NULL,  --�û��ǳ�
  Sex tinyint NULL CHECK(Sex = 0 OR Sex = 1 OR Sex = 2),  --�û��Ա�ֵΪ1ʱ�����ԣ�Ϊ2ʱ��Ů�ԣ�Ϊ0ʱ��δ֪
  City nvarchar(50) NULL,  --�û����ڳ���
  Country nvarchar(50) NULL,  --�û����ڹ���
  Province nvarchar(50) NULL,  --�û�����ʡ��
  Language nvarchar(50) NULL,  --�û�������
  HeadImgUrl nvarchar(500) NULL,  --�û�ͷ��ͼƬ����
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --�û��״���Ȩʱ�䣬�������ݿ⵱ʱʱ��
)


--�������Լ��
ALTER TABLE WXUserInfo ADD CONSTRAINT FK_WXUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users


--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', '�û�΢����Ϣ��<br/>���û�ͬ��΢����Ȩ��¼ʱ��ȡ��', 'user',@CurrentUser, 'table', 'WXUserInfo'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Openid', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Openid'
EXECUTE sp_addextendedproperty 'MS_Description', '�û��ǳ�', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'NickName'
EXECUTE sp_addextendedproperty 'MS_Description', '�û��Ա�ֵΪ1ʱ�����ԣ�Ϊ2ʱ��Ů�ԣ�Ϊ0ʱ��δ֪', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Sex'
EXECUTE sp_addextendedproperty 'MS_Description', '�û����ڳ���', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'City'
EXECUTE sp_addextendedproperty 'MS_Description', '�û����ڹ���', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Country'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�����ʡ��', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Province'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�������', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'Language'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�ͷ��ͼƬ����', 'user', @CurrentUser, 'table', 'WXUserInfo', 'column', 'HeadImgUrl'


/*
 * �û���׿��Ϣ��
 * ֻ���״�΢����Ȩ��¼����ֻ���ʱ��ȡ
 *
 * �����û������ڲ�ͬ�����ϵ�¼�����ǽ��û�Ψһ��ʶ�Ͱ�׿IDͬʱ����Ϊ������
 * ����⵽AndroidID����ʱ���ٴβ�������
*/
CREATE TABLE AndroidUserInfo
(
  ID uniqueidentifier,  --�û�Ψһ��ʶ������
  AndroidID varchar(50),  --��׿ID������
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --��ȡʱ�䣬�������ݿ⵱ʱ��ʱ��

  PRIMARY KEY(ID,AndroidID)  --���û���ʶ�Ͱ�׿IDͬʱ����Ϊ����
)

--�������Լ��
ALTER TABLE AndroidUserInfo ADD CONSTRAINT FK_AndroidUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users

--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', '�û���׿��Ϣ��<br/>ֻ���״�΢����Ȩ��¼����ֻ���ʱ��ȡ��<br/><br/>�����û������ڲ�ͬ�����ϵ�¼�����ǽ��û�Ψһ��ʶ�Ͱ�׿IDͬʱ����Ϊ������<br/>����⵽AndroidID����ʱ���ٴβ�������', 'user',@CurrentUser, 'table', 'AndroidUserInfo'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Openid', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'AndroidID'
EXECUTE sp_addextendedproperty 'MS_Description', '�û��ǳ�', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'CreateTime'


/*
 * ������
 * ��¼�û�������Ϣ
*/
CREATE TABLE Orders
(
  ID uniqueidentifier,  --�û�Ψһ��ʶ������
  OrderID char(16),  --����Ψһ��ʶ������:�������16λ�ַ�
  OrderType varchar(20) NOT NULL,  --����ҵ������ *** ֮���貹��Լ��  ***
  Price decimal(10,2) NOT NULL,  --�����۸�,��ȷ��λС��
  PayState varchar(10) NOT NULL,  --֧��״̬  *** ֮���貹��Լ�� ***
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --��������ʱ�䣬�������ݿ⵱ʱ��ʱ��

  PRIMARY KEY(ID,OrderID)  --���û���ʶ�Ͱ�׿IDͬʱ����Ϊ����
)

--�������Լ��
ALTER TABLE Orders ADD CONSTRAINT FK_Orders_Users FOREIGN KEY(ID)
REFERENCES dbo.Users



--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', '������<br/>��¼�û�������Ϣ<br/>���û�ID�Ͷ���IDͬʱ����Ϊ����','user',@CurrentUser, 'table', 'Orders';
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'Orders', 'column', 'ID';
EXECUTE sp_addextendedproperty 'MS_Description', '����Ψһ��ʶ', 'user', @CurrentUser, 'table', 'Orders', 'column', 'OrderID';
EXECUTE sp_addextendedproperty 'MS_Description', '����ҵ������*** ֮���貹��Լ��  ***', 'user', @CurrentUser, 'table', 'Orders', 'column', 'OrderType';
EXECUTE sp_addextendedproperty 'MS_Description', '�����۸�,��ȷ��λС��', 'user', @CurrentUser, 'table', 'Orders', 'column', 'Price';
EXECUTE sp_addextendedproperty 'MS_Description', '֧��״̬  *** ֮���貹��Լ�� ***  ***', 'user', @CurrentUser, 'table', 'Orders', 'column', 'PayState';
EXECUTE sp_addextendedproperty 'MS_Description', '��������ʱ�䣬�������ݿ⵱ʱ��ʱ��', 'user', @CurrentUser, 'table', 'Orders', 'column', 'CreateTime';

/*
 * ������ѯ��
 * ��¼��������ѯ�Ĺؼ�����Ϣ
*/
CREATE TABLE OrderSearch
(
  OrderID char(16) PRIMARY KEY,  --����Ψһ��ʶ������
  UserName nvarchar(20) NULL,  --��ѯ���� ��Ӧ���������ͱ���ȡ��
  Sex tinyint NULL CHECK(Sex = 0 OR Sex = 1 OR Sex = 2),  --�Ա� ֵΪ1ʱ�����ԣ�Ϊ2ʱ��Ů�ԣ�Ϊ0ʱ��δ֪ ��Ӧ���������ͱ���ȡ��
  BirthDay datetime NULL,  --�������� ��Ӧ���������ͱ���ȡ��
  ManName nvarchar(20) NULL,  --���ֺϻ��з�����
  ManBirthDay datetime NULL,  --���ֺϻ��з���������
  WomanName nvarchar(20) NULL,  --���ֺϻ�Ů������
  WomanBirthDay datetime NULL,  --���ֺϻ�Ů����������
)

--ALTER TABLE Orders ADD CONSTRAINT FK_Orders_OrderSearch FOREIGN KEY(OrderID)
--REFERENCES dbo.OrderSearch(OrderID)
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_OrderSearch] FOREIGN KEY([OrderID])
REFERENCES [dbo].[OrderSearch] ([OrderID])


--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', 'OrderSearch<br/>��¼��������ѯ�Ĺؼ�����Ϣ','user',@CurrentUser, 'table', 'OrderSearch';
EXECUTE sp_addextendedproperty 'MS_Description', '����Ψһ��ʶ', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'OrderID';
EXECUTE sp_addextendedproperty 'MS_Description', '��ѯ���� ��Ӧ���������ͱ���ȡ��', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'UserName';
EXECUTE sp_addextendedproperty 'MS_Description', '�Ա� ֵΪ1ʱ�����ԣ�Ϊ2ʱ��Ů�ԣ�Ϊ0ʱ��δ֪ ��Ӧ���������ͱ���ȡ��', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'Sex';
EXECUTE sp_addextendedproperty 'MS_Description', '�������� ��Ӧ���������ͱ���ȡ��', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'BirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ��з�����', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'ManName';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ��з���������', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'ManBirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ�Ů������', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'WomanName';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ�Ů����������', 'user', @CurrentUser, 'table', 'OrderSearch', 'column', 'WomanBirthDay';

/*
 * ������¼��
 * ��¼�û�����������Ϣ
*/
CREATE TABLE Searchs
(
  ID uniqueidentifier PRIMARY KEY,  --�û�Ψһ��ʶ������
  UserName nvarchar(20) NULL,  --��ѯ���� ��Ӧ���������ͱ���ȡ��
  Sex tinyint NULL CHECK(Sex = 0 OR Sex = 1 OR Sex = 2),  --�Ա� ֵΪ1ʱ�����ԣ�Ϊ2ʱ��Ů�ԣ�Ϊ0ʱ��δ֪ ��Ӧ���������ͱ���ȡ��
  BirthDay datetime NULL,  --�������� ��Ӧ���������ͱ���ȡ��
  ManName nvarchar(20) NULL,  --���ֺϻ��з�����
  ManBirthDay datetime NULL,  --���ֺϻ��з���������
  WomanName nvarchar(20) NULL,  --���ֺϻ�Ů������
  WomanBirthDay datetime NULL,  --���ֺϻ�Ů����������
  ZhouWord nvarchar(50) NULL,  --�ܹ�����������
)

--�������Լ��
ALTER TABLE Searchs ADD CONSTRAINT FK_Searchs_Users FOREIGN KEY(ID)
REFERENCES dbo.Users

--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', '������¼��<br/>��¼�û�����������Ϣ','user',@CurrentUser, 'table', 'Searchs';
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ID';
EXECUTE sp_addextendedproperty 'MS_Description', '��ѯ���� ��Ӧ���������ͱ���ȡ��', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'UserName';
EXECUTE sp_addextendedproperty 'MS_Description', '�Ա� ֵΪ1ʱ�����ԣ�Ϊ2ʱ��Ů�ԣ�Ϊ0ʱ��δ֪ ��Ӧ���������ͱ���ȡ��', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'Sex';
EXECUTE sp_addextendedproperty 'MS_Description', '�������� ��Ӧ���������ͱ���ȡ��', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'BirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ��з�����', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ManName';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ��з���������', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ManBirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ�Ů������', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'WomanName';
EXECUTE sp_addextendedproperty 'MS_Description', '���ֺϻ�Ů����������', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'WomanBirthDay';
EXECUTE sp_addextendedproperty 'MS_Description', '�ܹ�����������', 'user', @CurrentUser, 'table', 'Searchs', 'column', 'ZhouWord';

/*
 * ����
 * ��¼�û�������¼
*/
CREATE TABLE BurialPoint
(
  UUID uniqueidentifier PRIMARY KEY,  --Ψһ��ʶ������
  AndroidID varchar(50) NULL,  --��׿ID 
  ID uniqueidentifier NULL,  --�û�ID
  FType nvarchar(20) NULL,  --���һ������
  SType nvarchar(20) NULL,  --����������
  Bak nvarchar(100) NULL,  --�������˵�� ���������Ͳ�������������ʱ����������Ϣд�����
  CreateTime datetime NOT NULL DEFAULT GETDATE(),  --����ʱ�䣬�������ݿ⵱ʱ��ʱ��
)

--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', '����<br/>��¼�û�������¼','user',@CurrentUser, 'table', 'BurialPoint';
EXECUTE sp_addextendedproperty 'MS_Description', 'Ψһ��ʶ', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'UUID';
EXECUTE sp_addextendedproperty 'MS_Description', '��׿ID', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'AndroidID';
EXECUTE sp_addextendedproperty 'MS_Description', '�û�ID', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'ID';
EXECUTE sp_addextendedproperty 'MS_Description', '���һ������', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'FType';
EXECUTE sp_addextendedproperty 'MS_Description', '����������', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'SType';
EXECUTE sp_addextendedproperty 'MS_Description', '�������˵�������������Ͳ�������������ʱ����������Ϣд�����', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'Bak';
EXECUTE sp_addextendedproperty 'MS_Description', '����ʱ�䣬�������ݿ⵱ʱ��ʱ��', 'user', @CurrentUser, 'table', 'BurialPoint', 'column', 'CreateTime';

/*
 * ������־��
 * ��¼����Ĵ�����Ϣ
*/
CREATE TABLE Logs
(
 UUID uniqueidentifier PRIMARY KEY,  --Ψһ��ʶ������
 msg nvarchar(1000) NULL,  --������Ϣ
 StackTrace nvarchar(MAX) NULL,  --��ջ�켣
 CreateTime datetime NOT NULL DEFAULT GETDATE(),  --����ʱ�䣬�������ݿ⵱ʱ��ʱ��
)

--����˵��
EXECUTE sp_addextendedproperty 'MS_Description', '������־��<br/>��¼����Ĵ�����Ϣ','user',@CurrentUser, 'table', 'Logs';
EXECUTE sp_addextendedproperty 'MS_Description', 'Ψһ��ʶ', 'user', @CurrentUser, 'table', 'Logs', 'column', 'UUID';
EXECUTE sp_addextendedproperty 'MS_Description', '������Ϣ', 'user', @CurrentUser, 'table', 'Logs', 'column', 'msg';
EXECUTE sp_addextendedproperty 'MS_Description', '��ջ�켣', 'user', @CurrentUser, 'table', 'Logs', 'column', 'StackTrace';
EXECUTE sp_addextendedproperty 'MS_Description', '����ʱ�䣬�������ݿ⵱ʱ��ʱ��', 'user', @CurrentUser, 'table', 'Logs', 'column', 'CreateTime';

USE master; --�˳�ռ��



--/*
-- * ��¼��־��
-- * ��¼�û���¼����
--*/
--CREATE TABLE Logins
--(
--  ID uniqueidentifier PRIMARY KEY,  --�û�Ψһ��ʶ������
--  LoginType tinyint NOT NULL CHECK(LoginType = 0 OR LoginType = 1),  --��¼����  0�����˺������¼��1����΢����Ȩ��¼�������Ժ���չ��������Ϊtinyint��
--  CreateTime datetime NOT NULL  --��¼ʱ�䣬�������ݿ⵱ʱ��ʱ��
--)

----�������Լ��
--ALTER TABLE Logins ADD CONSTRAINT FK_Logins_Users FOREIGN KEY(ID)
--REFERENCES dbo.Users

----����˵��
--EXECUTE sp_addextendedproperty 'MS_Description', '��¼��־��<br/>��¼�û���¼����','user',@CurrentUser, 'table', 'Logins';
--EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'Logins', 'column', 'ID';
--EXECUTE sp_addextendedproperty 'MS_Description', '��¼����  0�����˺������¼��1����΢����Ȩ��¼�������Ժ���չ��������Ϊtinyint��', 'user', @CurrentUser, 'table', 'Logins', 'column', 'LoginType';
--EXECUTE sp_addextendedproperty 'MS_Description', '��¼ʱ�䣬�������ݿ⵱ʱ��ʱ��', 'user', @CurrentUser, 'table', 'Logins', 'column', 'CreateTime';

--/*
-- * ע����־��
-- * ��¼�û�ע������
--*/
--CREATE TABLE UnLogins
--(
--  ID uniqueidentifier PRIMARY KEY,  --�û�Ψһ��ʶ������
--  CreateTime datetime NOT NULL  --ע��ʱ�䣬�������ݿ⵱ʱ��ʱ��
--)

----�������Լ��
--ALTER TABLE UnLogins ADD CONSTRAINT FK_UnLogins_Users FOREIGN KEY(ID)
--REFERENCES dbo.Users

----����˵��
--EXECUTE sp_addextendedproperty 'MS_Description', 'ע����־��<br/>��¼�û�ע������','user',@CurrentUser, 'table', 'UnLogins';
--EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'UnLogins', 'column', 'ID';
--EXECUTE sp_addextendedproperty 'MS_Description', 'ע��ʱ�䣬�������ݿ⵱ʱ��ʱ��', 'user', @CurrentUser, 'table', 'UnLogins', 'column', 'CreateTime';