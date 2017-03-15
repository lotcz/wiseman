<?php
	$this->setPageTitle('origins');	
	$this->renderAdminTable(
		'origins', 		
		'origin',
		[	
			[
				'name' => 'origin_name',
				'label' => 'Author name'			
			],
			[
				'name' => 'origin_description',
				'label' => 'Description'			
			],
			[
				'name' => 'origin_total_quotes',
				'label' => 'Quotes'			
			]
		]
	);