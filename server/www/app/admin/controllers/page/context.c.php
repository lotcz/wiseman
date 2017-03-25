<?php
	require_once __DIR__ . '/../../../models/context.m.php';
	
	$this->renderAdminForm(
		'context',
		'ContextModel',
		[		
			[
				'name' => 'context_name',
				'label' => 'Name',
				'type' => 'text',
				'validations' => [['type' => 'length', 'param' => 1], ['type' => 'maxlen', 'param' => 200]]
			],
			[
				'name' => 'context_description',
				'label' => 'Description',
				'type' => 'text'
			],
			[
				'name' => 'context_language_id',
				'label' => 'Language',
				'type' => 'select',
				'select_table' => 'languages',
				'select_id_field' => 'language_id',
				'select_label_field' => 'language_name'
			]
		]
	);
	
	