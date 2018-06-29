DELETE FROM park;
DELETE FROM campground;
DELETE FROM site;
DELETE FROM reservation;

-- Insert a fake park

SET IDENTITY_INSERT park ON;
INSERT INTO park (park_id, name, location, establish_date, area, visitors, discription) VALUES (1, 'FAKEASS PARK', 'fake state', 20018-7-2, 13000, 10000, 'These are fact I just made up about this park...story has it this is the place where Chewbacca won a cage match against Big Foot');
SET IDENTITY_INSERT park OFF;

-- Insert a fake campground

SET IDENTITY_INSERT campground ON;
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (1, 1, 'Hidden Valley Ranch', 1, 12, 40);
SET IDENTITY_INSERT campground OFF;

--Insert fake site
SET IDENTITY_INSERT site ON;
INSERT INTO site (site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities) VALUES (1, 1, 10, 20, 1, 40, 0);
SET IDENTITY_INSERT site OFF;

--insert fake reservation 
SET IDENTITY_INSERT reservation ON;
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date, create_date) VALUES (1, 1, 'Jimbo SmitHammer', 2018-07-07, 2018-07-12, 2018-06-07);
SET IDENTITY_INSERT reservation OFF;

