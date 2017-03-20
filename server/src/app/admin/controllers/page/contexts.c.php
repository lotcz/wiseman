<?php
	$this->setPageTitle('Contexts');	
	$this->renderAdminTable(
		'viewContexts', 		
		'context',
		[		
			[
				'name' => 'context_name',
				'label' => 'Name'			
			],
			[
				'name' => 'context_description',
				'label' => 'Description'			
			],
			[
				'name' => 'language_name',
				'label' => 'Language'			
			],
			[
				'name' => 'context_total_quotes',
				'label' => 'Quotes'
			],
		]
	);