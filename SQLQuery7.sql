select * from Quiz

-- Quiz που γίνανε ανά καφάλαιο και χρήστη
select ChapterId, CustomerId, count(*) 
from Quiz Qz
Group by Qz.ChapterId,CustomerId
Order by Qz.ChapterId

-- Quiz που γίνανε ανά καφάλαιο και δεδομένο χρήστη
select ChapterId, CustomerId, count(*) 
from Quiz Qz
Where Qz.CustomerId = 4
Group by Qz.ChapterId,CustomerId
Order by Qz.ChapterId
