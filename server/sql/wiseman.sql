DROP DATABASE IF EXISTS wiseman;
CREATE DATABASE wiseman DEFAULT char set utf8;
USE wiseman;

/*
	RUN zEngine.sql!
*/

CREATE TABLE IF NOT EXISTS `link_types` (
  `link_type_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `link_type_name` VARCHAR(150) NOT NULL,
  `link_type_icon` VARCHAR(100) NULL,
  `link_type_url_template` VARCHAR(200) NULL,
  PRIMARY KEY (`link_type_id`)  
) ENGINE = InnoDB;

INSERT INTO `link_types` VALUES (NULL, 'General link', NULL, '%s');
INSERT INTO `link_types` VALUES (NULL, 'Wiseman author link', NULL, 'http://wiseman.loc/authors/%s');
INSERT INTO `link_types` VALUES (NULL, 'Wiseman origin link', NULL, 'http://wiseman.loc/origins/%s');
INSERT INTO `link_types` VALUES (NULL, 'Wikipedia - English', NULL, 'http://en.wikipedia.org/wiki/%s');

CREATE TABLE IF NOT EXISTS `contexts` (
  `context_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `context_name` NVARCHAR(200) NOT NULL,
  `context_description` TEXT NULL,
  `context_total_quotes` INT UNSIGNED NOT NULL DEFAULT 0,
  `context_language_id` TINYINT UNSIGNED NOT NULL,

  PRIMARY KEY (`context_id`),  
  CONSTRAINT `context_language_fk`
    FOREIGN KEY (`context_language_id`)
    REFERENCES `languages` (`language_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `authors` (
  `author_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `author_context_id` INT UNSIGNED NOT NULL,
  `author_name` VARCHAR(150) NOT NULL,  
  `author_description` TEXT,  
  `author_link_url` VARCHAR(200) NULL,
  `author_link_type_id` INT UNSIGNED NOT NULL DEFAULT 1,
  `author_total_quotes` INT UNSIGNED NOT NULL DEFAULT 0,

  PRIMARY KEY (`author_id`),
  CONSTRAINT `author_context_fk`
    FOREIGN KEY (`author_context_id`)
    REFERENCES `contexts` (`context_id`),
  CONSTRAINT `author_link_type_fk`
    FOREIGN KEY (`author_link_type_id`)
    REFERENCES `link_types` (`link_type_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `origins` (
  `origin_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `origin_context_id` INT UNSIGNED NOT NULL,
  `origin_name` NVARCHAR(200) NOT NULL,
  `origin_description` TEXT NULL,
  `origin_total_quotes` INT UNSIGNED NOT NULL DEFAULT 0,  

  PRIMARY KEY (`origin_id`),  
  CONSTRAINT `origin_context_fk`
    FOREIGN KEY (`origin_context_id`)
    REFERENCES `contexts` (`context_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `quotes` (
  `quote_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `quote_deleted` BOOL DEFAULT 0 NOT NULL,  
  `quote_text` TEXT NOT NULL,  
  `quote_views` INT UNSIGNED NOT NULL DEFAULT 0,
  `quote_context_id` INT UNSIGNED NOT NULL,
  `quote_origin_id` INT UNSIGNED NULL,
  `quote_author_id` INT UNSIGNED NULL,

  PRIMARY KEY (`quote_id`), 
  CONSTRAINT `quote_context_fk`
    FOREIGN KEY (`quote_context_id`)
    REFERENCES `contexts` (`context_id`),
  CONSTRAINT `quote_origin_fk`
    FOREIGN KEY (`quote_origin_id`)
    REFERENCES `origins` (`origin_id`),
  CONSTRAINT `quote_author_fk`
    FOREIGN KEY (`quote_author_id`)
    REFERENCES `authors` (`author_id`)
) ENGINE = InnoDB;

 DROP PROCEDURE IF EXISTS spUpdateContextQuoteCount;
 
DELIMITER //
 CREATE PROCEDURE spUpdateContextQuoteCount(IN con_id INT UNSIGNED)
	BEGIN
		DECLARE total INT DEFAULT 0;
		SELECT COUNT(*) INTO total
		FROM quotes WHERE quote_context_id = con_id AND quote_deleted = 0;
		UPDATE contexts SET context_total_quotes = total WHERE context_id = con_id;
   END //
DELIMITER ;
 
DROP PROCEDURE IF EXISTS spUpdateoriginQuoteCount;
 
DELIMITER //
 CREATE PROCEDURE spUpdateOriginQuoteCount(IN s_id INT UNSIGNED)
	BEGIN
		DECLARE total INT DEFAULT 0;
		SELECT COUNT(*) INTO total
		FROM quotes WHERE quote_origin_id = s_id AND quote_deleted = 0;
		UPDATE origins SET origin_total_quotes = total WHERE origin_id = s_id;
   END //
DELIMITER ;
 
 DROP PROCEDURE IF EXISTS spUpdateAuthorQuoteCount;
 
DELIMITER //
 CREATE PROCEDURE spUpdateAuthorQuoteCount(IN a_id INT UNSIGNED)
	BEGIN
		DECLARE total INT DEFAULT 0;
		SELECT COUNT(*) INTO total
		FROM quotes WHERE quote_author_id = a_id AND quote_deleted = 0;
		UPDATE `authors` SET author_total_quotes = total WHERE author_id = a_id;
   END //
DELIMITER ;

 DROP TRIGGER IF EXISTS update_quote_count_trigger_upd;
 
 DELIMITER //
 CREATE TRIGGER update_quote_count_trigger_upd AFTER UPDATE ON quotes
	FOR EACH ROW
		BEGIN
			CALL spUpdateContextQuoteCount(OLD.quote_context_id);            
            CALL spUpdateContextQuoteCount(NEW.quote_context_id);    
           
            CALL spUpdateOriginQuoteCount(OLD.quote_origin_id);                   
            CALL spUpdateOriginQuoteCount(NEW.quote_origin_id);
            
            CALL spUpdateAuthorQuoteCount(OLD.quote_author_id);                   
            CALL spUpdateAuthorQuoteCount(NEW.quote_author_id);
		END //
 DELIMITER ;
 
 DROP TRIGGER IF EXISTS update_quote_count_trigger_ins;
  
 DELIMITER //
 CREATE TRIGGER update_quote_count_trigger_ins AFTER INSERT ON quotes
	FOR EACH ROW
		BEGIN
            CALL spUpdateContextQuoteCount(NEW.quote_context_id);
            CALL spUpdateOriginQuoteCount(NEW.quote_origin_id);            
            CALL spUpdateAuthorQuoteCount(NEW.quote_author_id);
		END //
 DELIMITER ;
 
DROP TRIGGER IF EXISTS update_quote_count_trigger_del;
  
 DELIMITER //
 CREATE TRIGGER update_quote_count_trigger_del AFTER DELETE ON quotes
	FOR EACH ROW
		BEGIN
            CALL spUpdateContextQuoteCount(OLD.quote_context_id);            
            CALL spUpdateOriginQuoteCount(OLD.quote_origin_id);
            CALL spUpdateAuthorQuoteCount(OLD.quote_author_id);
		END //
 DELIMITER ;

DROP VIEW IF EXISTS `viewContexts`;

CREATE VIEW viewContexts AS
	SELECT *
    FROM contexts c
    LEFT OUTER JOIN languages l ON (c.context_language_id = l.language_id);
    
DROP VIEW IF EXISTS `viewQuotes`;

CREATE VIEW viewQuotes AS
	SELECT *
    FROM quotes q
    LEFT OUTER JOIN contexts c ON (c.context_id = q.quote_context_id)
    LEFT OUTER JOIN origins s ON (s.origin_id = q.quote_origin_id)
    LEFT OUTER JOIN `authors` a ON (a.author_id = q.quote_author_id);
