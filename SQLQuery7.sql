select * from Quiz

-- Quiz ��� ������ ��� �������� ��� ������
select ChapterId, CustomerId, count(*) 
from Quiz Qz
Group by Qz.ChapterId,CustomerId
Order by Qz.ChapterId

-- Quiz ��� ������ ��� �������� ��� �������� ������
select ChapterId, CustomerId, count(*) 
from Quiz Qz
Where Qz.CustomerId = 4
Group by Qz.ChapterId,CustomerId
Order by Qz.ChapterId

select ChapterId, CustomerId, Tries, count(*) 
from Quiz Qz
Where Qz.CustomerId = 4
Group by Qz.ChapterId,CustomerId,Tries
Order by Qz.ChapterId
