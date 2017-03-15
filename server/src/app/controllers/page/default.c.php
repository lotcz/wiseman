<?php

	require_once __DIR__ . '/../../models/quote.m.php';
	$this->setPageTitle('Main Page');
	$this->requireClass('paging');
	
	$quote = QuoteModel::getRandom($this->db);
	if (isset($quote)) {
		$this->setData('random_quote', $quote);	
		
		//update number of views
		$q = new QuoteModel($this->db);		
		$q->set('quote_views', $quote->ival('quote_views',0) + 1);
		$q->set('quote_id', $quote->ival('quote_id'));
		$q->save();
		
	}
	