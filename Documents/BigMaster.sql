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
GO

/*
 * �û���
 * ΢����Ȩ��¼Ҳ���������������ݣ���ʱֻд���û�Ψһ��ʶ��Ϣ����������Ȩ�û����ֻ���������д��������Ϣ��
*/
CREATE TABLE Users
(
  ID uniqueidentifier PRIMARY KEY,  --�û�Ψһ��ʶ������
  Phone varchar(50) NULL,  --�ֻ�����
  Password varchar(50) NULL,  --��¼����
  Salt char(6) NULL,  --��ֵ
  SaltPassword char(40) NULL, --���ܺ�����
  RegisterTime datetime NULL,  --�״�ע��ʱ��
  CreateTime datetime not NULL DEFAULT GETDATE(),  --�״���Ȩ��ע��ʱ�䣬�������ݿ⵱ʱ��ʱ��
)
GO

--���˵��
DECLARE @CurrentUser sysname;
SELECT @CurrentUser = USER_NAME();
EXECUTE sp_addextendedproperty 'MS_Description', '�û���<br/>΢����Ȩ��¼Ҳ���������������ݣ���ʱֻд���û�Ψһ��ʶ��Ϣ����������Ȩ�û����ֻ���������д��������Ϣ��', 'user',@CurrentUser, 'table', 'Users'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'Users', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '�ֻ�����', 'user', @CurrentUser, 'table', 'Users', 'column', 'Phone'
EXECUTE sp_addextendedproperty 'MS_Description', '��¼����', 'user', @CurrentUser, 'table', 'Users', 'column', 'Password'
EXECUTE sp_addextendedproperty 'MS_Description', '��ֵ', 'user', @CurrentUser, 'table', 'Users', 'column', 'Salt'
EXECUTE sp_addextendedproperty 'MS_Description', '���ܺ�����', 'user', @CurrentUser, 'table', 'Users', 'column', 'SaltPassword'
EXECUTE sp_addextendedproperty 'MS_Description', '�״�ע��ʱ��', 'user', @CurrentUser, 'table', 'Users', 'column', 'RegisterTime'
EXECUTE sp_addextendedproperty 'MS_Description', '�״���Ȩ��ע��ʱ�䣬�������ݿ⵱ʱ��ʱ��', 'user', @CurrentUser, 'table', 'Users', 'column', 'CreateTime'
GO

/*
 * �û�΢����Ϣ��
 * ���û�ͬ��΢����Ȩ��¼ʱ��ȡ
*/
CREATE TABLE WXUserInfo
(
  ID uniqueidentifier PRIMARY KEY,  --�û�Ψһ��ʶ������
  Openid varchar(50) NULL,  --�û�Openid
  NickName nvarchar(50) NULL,  --�û��ǳ�
  Sex tinyint NULL,  --�û��Ա�ֵΪ1ʱ�����ԣ�Ϊ2ʱ��Ů�ԣ�Ϊ0ʱ��δ֪
  City nvarchar(50) NULL,  --�û����ڳ���
  Country nvarchar(50) NULL,  --�û����ڹ���
  Province nvarchar(50) NULL,  --�û�����ʡ��
  Language nvarchar(50) NULL,  --�û�������
  HeadImgUrl nvarchar(500) NULL,  --�û�ͷ��ͼƬ����
)
GO

--������Լ��
ALTER TABLE WXUserInfo ADD CONSTRAINT FK_WXUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users
GO

--���˵��
DECLARE @CurrentUser sysname;
SELECT @CurrentUser = USER_NAME();
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
GO

/*
 * �û���׿��Ϣ��
 * ֻ���״�΢����Ȩ��¼����ֻ���ʱ��ȡ
 *
 * �����û������ڲ�ͬ�����ϵ�¼�����ǽ��û�Ψһ��ʶ�Ͱ�׿IDͬʱ����Ϊ������
 * ����⵽AndroidID����ʱ���ٴβ�������
*/
CREATE TABLE AndroidUserInfo
(
  ID uniqueidentifier not NULL,  --�û�Ψһ��ʶ������
  AndroidID varchar(50) not NULL,  --��׿ID
  CreateTime datetime not NULL DEFAULT GETDATE(),  --��ȡʱ�䣬�������ݿ⵱ʱ��ʱ��

  PRIMARY KEY(ID,AndroidID)  --���û���ʶ�Ͱ�׿IDͬʱ����Ϊ����
)
GO

--������Լ��
ALTER TABLE AndroidUserInfo ADD CONSTRAINT FK_AndroidUserInfo_Users FOREIGN KEY(ID)
REFERENCES dbo.Users
GO

--���˵��
DECLARE @CurrentUser sysname;
SELECT @CurrentUser = USER_NAME();
EXECUTE sp_addextendedproperty 'MS_Description', '�û���׿��Ϣ��<br/>ֻ���״�΢����Ȩ��¼����ֻ���ʱ��ȡ��<br/><br/>�����û������ڲ�ͬ�����ϵ�¼�����ǽ��û�Ψһ��ʶ�Ͱ�׿IDͬʱ����Ϊ������<br/>����⵽AndroidID����ʱ���ٴβ�������', 'user',@CurrentUser, 'table', 'AndroidUserInfo'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Ψһ��ʶ', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'ID'
EXECUTE sp_addextendedproperty 'MS_Description', '�û�Openid', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'AndroidID'
EXECUTE sp_addextendedproperty 'MS_Description', '�û��ǳ�', 'user', @CurrentUser, 'table', 'AndroidUserInfo', 'column', 'CreateTime'
GO

USE master; --�˳�ռ��