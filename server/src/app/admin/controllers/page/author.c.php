<?php
	require_once __DIR__ . '/../../../models/author.m.php';
	
	$this->renderAdminForm(
		'author',
		'AuthorModel',
		[	
			[
				'name' => 'author_context_id',
				'label' => 'Context',
				'type' => 'select',
				'select_table' => 'contexts',
				'select_id_field' => 'context_id',
				'select_label_field' => 'context_name'
			],
			[
				'name' => 'author_name',
				'label' => 'Author name',
				'type' => 'text',
				'validations' => [['type' => 'length', 'param' => 1]]
			],
			[
				'name' => 'author_description',
				'label' => 'Description',
				'type' => 'text'				
			],
			[
				'name' => 'author_total_quotes',
				'label' => 'Quotes',
				'type' => 'static'				
			]
		]
	);
	
	