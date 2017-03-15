<?php

class QuoteModel extends zModel {
	
	public $table_name = 'quotes';
	public $id_name = 'quote_id';
	
	static function getRandom($db) {		
		$top = Self::select(
			/* db */		$db, 
			/* table */		'viewQuotes', 
			/* where */		null,
			/* bindings */	null,
			/* types */		null,
			/* paging */	new zPaging(0,1),
			/* orderby */	'RAND()'
		);
		if (count($top) > 0) {
			return $top[0];	
		}		
	}
}