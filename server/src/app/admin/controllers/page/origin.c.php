<?php
	require_once __DIR__ . '/../../../models/origin.m.php';
	
	$this->renderAdminForm(
		'origin',
		'OriginModel',
		[
			[
				'name' => 'origin_context_id',
				'label' => 'Context',
				'type' => 'select',
				'select_table' => 'contexts',
				'select_id_field' => 'context_id',
				'select_label_field' => 'context_name'
			],
			[
				'name' => 'origin_name',
				'label' => 'origin name',
				'type' => 'text',
				'validations' => [['type' => 'length', 'param' => 1]]
			],
			[
				'name' => 'origin_description',
				'label' => 'Description',
				'type' => 'text'				
			],
			[
				'name' => 'origin_total_quotes',
				'label' => 'Quotes',
				'type' => 'static'				
			]
		]
	);
	
	