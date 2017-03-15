<?php
	require_once __DIR__ . '/../../../models/quote.m.php';
	
	$this->renderAdminForm(
		'quote',
		'QuoteModel',
		[	
			[
				'name' => 'quote_context_id',
				'label' => 'Context',
				'type' => 'select',
				'select_table' => 'contexts',
				'select_id_field' => 'context_id',
				'select_label_field' => 'context_name'
			],
			[
				'name' => 'quote_text',
				'label' => 'Quote text',
				'type' => 'text',
				'validations' => [['type' => 'length', 'param' => 1]]
			],
			[
				'name' => 'quote_author_id',
				'label' => 'Author',
				'type' => 'select',
				'select_table' => 'authors',
				'select_id_field' => 'author_id',
				'select_label_field' => 'author_name'
			],			
			[
				'name' => 'quote_origin_id',
				'label' => 'Origin',
				'type' => 'select',
				'select_table' => 'origins',
				'select_id_field' => 'origin_id',
				'select_label_field' => 'origin_name'
			],
			[
				'name' => 'quote_deleted',
				'label' => 'Deleted',
				'type' => 'bool'
			],
			[
				'name' => 'quote_views',
				'label' => 'Views',
				'type' => 'static'
			],
		]
	);
	
	