<?php
	$this->setPageTitle('Quotes');	
	$this->renderAdminTable(
		'viewQuotes', 		
		'quote',
		[	
			[
				'name' => 'context_name',
				'label' => 'Context'			
			],
			[
				'name' => 'quote_text',
				'label' => 'Quote text'			
			],
			[
				'name' => 'author_name',
				'label' => 'Author'			
			],
			[
				'name' => 'origin_name',
				'label' => 'Origin'			
			],
			[
				'name' => 'quote_views',
				'label' => 'Views'			
			]	
		]
	);