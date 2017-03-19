<?php

	require_once __DIR__ . '/../../models/quote.m.php';
	$this->requireClass('paging');
	$quote = QuoteModel::getRandom($this->db);
	
	//update number of views
	$q = new QuoteModel($this->db);		
	$q->set('quote_views', $quote->ival('quote_views',0) + 1);
	$q->set('quote_id', $quote->ival('quote_id'));
	$q->save();
		
	//!!!!
	sleep(3);
	
	$this->setData('quote', $quote->data);