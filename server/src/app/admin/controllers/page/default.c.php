<?php
	require_once __DIR__ . '/../../../models/quote.m.php';
	
	$items = QuoteModel::all($this->db);
	
	$this->data['count'] = count($items);