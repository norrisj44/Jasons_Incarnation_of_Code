DECLARE @arrival datetime = '2018-02-20'
DECLARE @departure datetime = '2018-02-23'
DECLARE @campgroundID int = 1

SELECT * FROM site
WHERE campground_id = @campgroundID
AND site.site_id NOT IN (
SELECT site.site_id FROM site
JOIN reservation ON site.site_id = reservation.site_id
WHERE reservation.from_date BETWEEN @arrival AND @departure
OR reservation.to_date BETWEEN @arrival AND @departure
OR (reservation.from_date < @arrival AND reservation.to_date > @departure)
)

--Acceptable site_id's within date range 2/20/18 - 2/23/18
--4	5	6	8	10 11	12
--Not acceptable:
--1	2	3	7	9
