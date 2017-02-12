DROP DATABASE IF EXISTS wiseman;
CREATE DATABASE wiseman DEFAULT char set utf8;
USE wiseman;

CREATE TABLE IF NOT EXISTS `ip_failed_attempts` (
  `ip_failed_attempt_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `ip_failed_attempt_ip` VARCHAR(15),
  `ip_failed_attempt_count` INT UNSIGNED NOT NULL DEFAULT 1,
  `ip_failed_attempt_first` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ip_failed_attempt_last` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ip_failed_attempt_id`),
   UNIQUE INDEX `ip_failed_attempt_ip_unique` (`ip_failed_attempt_ip` ASC)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `languages` (
  `language_id` TINYINT UNSIGNED NOT NULL AUTO_INCREMENT,
  `language_name` VARCHAR(100) NOT NULL,
  `language_code` VARCHAR(5) NOT NULL,
  `language_date_format` VARCHAR(50) NOT NULL,
  `language_datetime_format` VARCHAR(100) NOT NULL,
  `language_decimal_separator` VARCHAR(10) NOT NULL,
  `language_thousands_separator` VARCHAR(10) NOT NULL,  
  PRIMARY KEY (`language_id`)  
) ENGINE = InnoDB;

INSERT INTO languages VALUES (NULL, 'English','en','j/n/Y','j/n/Y h:i:s', '.', ',');
INSERT INTO languages VALUES (NULL, 'Čeština','cs','j.n.Y','j.n.Y h:i:s', ',', '&nbsp');

CREATE TABLE IF NOT EXISTS `users` (
  `user_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `user_deleted` BIT DEFAULT 0 NOT NULL,
  `user_is_superuser` BIT DEFAULT 0 NOT NULL,
  `user_login` VARCHAR(50),
  `user_email` VARCHAR(50) NOT NULL,
  `user_password_hash` VARCHAR(255) NULL,
  `user_failed_attempts` INT NOT NULL DEFAULT 0,
  `user_last_access` TIMESTAMP,
  `user_reset_password_hash` VARCHAR(255) NULL,
  `user_reset_password_until` TIMESTAMP NULL,
  `user_language_id` TINYINT UNSIGNED NOT NULL,
 
  PRIMARY KEY (`user_id`),
  UNIQUE INDEX `users_email_unique` (`user_email` ASC),
  UNIQUE INDEX `users_login_unique` (`user_login` ASC),
  CONSTRAINT `user_language_fk`
    FOREIGN KEY (`user_language_id`)
    REFERENCES `languages` (`language_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `user_sessions` (
  `user_session_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `user_session_token_hash` VARCHAR(255) NOT NULL,
  `user_session_user_id` INT(10) UNSIGNED NOT NULL,
  `user_session_created` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `user_session_expires` TIMESTAMP NOT NULL,
  PRIMARY KEY (`user_session_id`),
  CONSTRAINT `user_session_user_fk`
    FOREIGN KEY (`user_session_user_id`)
    REFERENCES `users` (`user_id`)
    ON DELETE CASCADE
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `link_types` (
  `link_type_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `link_type_name` VARCHAR(150) NOT NULL,
  `link_type_icon` VARCHAR(100) NULL,
  `link_type_url_template` VARCHAR(200) NULL,
  PRIMARY KEY (`link_type_id`)  
) ENGINE = InnoDB;

INSERT INTO `link_types` VALUES (NULL, 'General link', NULL, '%s');
INSERT INTO `link_types` VALUES (NULL, 'Wikipedia - English', NULL, 'http://en.wikipedia.org/wiki/%s');

CREATE TABLE IF NOT EXISTS `authors` (
  `author_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `author_name` VARCHAR(150) NOT NULL,
  `author_description` TEXT,
  `author_link_url` VARCHAR(200) NULL,
  `author_link_type_id` INT UNSIGNED NOT NULL DEFAULT 1,
  `author_total_quotes` INT UNSIGNED NOT NULL DEFAULT 0,
  PRIMARY KEY (`author_id`),
  CONSTRAINT `author_link_type_fk`
    FOREIGN KEY (`author_link_type_id`)
    REFERENCES `link_types` (`link_type_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `sources` (
  `source_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `source_name` NVARCHAR(200) NOT NULL,
  `source_description` TEXT NULL,
  `source_total_quotes` INT UNSIGNED NOT NULL DEFAULT 0,
  `source_language_id` TINYINT UNSIGNED NOT NULL,

  PRIMARY KEY (`source_id`),  
  CONSTRAINT `source_language_fk`
    FOREIGN KEY (`source_language_id`)
    REFERENCES `languages` (`language_id`)
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `contexts` (
  `context_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `context_parent_id` INT UNSIGNED,
  `context_name` NVARCHAR(200) NOT NULL,
  `context_description` TEXT NULL,
  `context_total_quotes` INT UNSIGNED NOT NULL DEFAULT 0,
  `context_language_id` TINYINT UNSIGNED NOT NULL,

  PRIMARY KEY (`context_id`),  
  CONSTRAINT `context_language_fk`
    FOREIGN KEY (`context_language_id`)
    REFERENCES `languages` (`language_id`), 
  INDEX `context_parent_id_index` (`context_parent_id` ASC),
  CONSTRAINT `context_parent_fk`
  FOREIGN KEY (`context_parent_id`)
    REFERENCES `contexts` (`context_id`)
    ON DELETE SET NULL
) ENGINE = InnoDB;

CREATE TABLE IF NOT EXISTS `quotes` (
  `quote_id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `quote_deleted` BOOL DEFAULT 0 NOT NULL,  
  `quote_text` TEXT NOT NULL,  
  `quote_views` INT UNSIGNED NOT NULL DEFAULT 0,
  `quote_context_id` INT UNSIGNED NOT NULL,
  `quote_source_id` INT UNSIGNED NULL,
  `quote_author_id` INT UNSIGNED NULL,

  PRIMARY KEY (`quote_id`), 
  CONSTRAINT `quote_context_fk`
    FOREIGN KEY (`quote_context_id`)
    REFERENCES `contexts` (`context_id`),
  CONSTRAINT `quote_source_fk`
    FOREIGN KEY (`quote_source_id`)
    REFERENCES `sources` (`source_id`)
    ON DELETE SET NULL
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
 
DROP PROCEDURE IF EXISTS spUpdateSourceQuoteCount;
 
DELIMITER //
 CREATE PROCEDURE spUpdateSourceQuoteCount(IN s_id INT UNSIGNED)
	BEGIN
		DECLARE total INT DEFAULT 0;
		SELECT COUNT(*) INTO total
		FROM quotes WHERE quote_source_id = s_id AND quote_deleted = 0;
		UPDATE sources SET source_total_quotes = total WHERE source_id = s_id;
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
           
            CALL spUpdateSourceQuoteCount(OLD.quote_source_id);                   
            CALL spUpdateSourceQuoteCount(NEW.quote_source_id);
            
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
            CALL spUpdateSourceQuoteCount(NEW.quote_source_id);            
            CALL spUpdateAuthorQuoteCount(NEW.quote_author_id);
		END //
 DELIMITER ;
 
DROP TRIGGER IF EXISTS update_quote_count_trigger_del;
  
 DELIMITER //
 CREATE TRIGGER update_quote_count_trigger_del AFTER DELETE ON quotes
	FOR EACH ROW
		BEGIN
            CALL spUpdateContextQuoteCount(OLD.quote_context_id);            
            CALL spUpdateSourceQuoteCount(OLD.quote_source_id);
            CALL spUpdateAuthorQuoteCount(OLD.quote_author_id);
		END //
 DELIMITER ;
 
DROP VIEW IF EXISTS `viewQuotes`;

CREATE VIEW viewQuotes AS
	SELECT *
    FROM quotes q
    LEFT OUTER JOIN contexts c ON (c.context_id = q.quote_context_id)
    LEFT OUTER JOIN sources s ON (s.source_id = q.quote_source_id)
    LEFT OUTER JOIN `authors` a ON (a.author_id = q.quote_author_id);
