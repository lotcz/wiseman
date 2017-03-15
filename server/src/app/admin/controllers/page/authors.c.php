<?php
	$this->setPageTitle('Authors');	
	$this->renderAdminTable(
		'authors', 		
		'author',
		[	
			[
				'name' => 'author_name',
				'label' => 'Author name'			
			],
			[
				'name' => 'author_description',
				'label' => 'Description'			
			],
			[
				'name' => 'author_total_quotes',
				'label' => 'Quotes'			
			]
		]
	);