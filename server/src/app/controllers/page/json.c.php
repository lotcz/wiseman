<?php

	$quotes = zSqlQuery::selectToArray(
		$this->db, 
		'viewQuotes',
		null, /* where */
		null, /* bindings */
		null, /* types */
		null, /* paging */
		'quote_text' /* orderby */
	);
	
	$this->setData('json', $quotes);