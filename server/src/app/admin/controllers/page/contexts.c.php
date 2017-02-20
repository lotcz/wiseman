<?php

	$this->page_title = $this->t('Contexts');
	$this->templates['page'] = 'admin';
	
	$table = new zAdminTable(
		'contexts', 		
		'context'
	);
	
	$table->add([		
		[
			'name' => 'context_name',
			'label' => 'Name'			
		]
	]);
	
	$table->prepare($this->db);
	
	$this->data['table'] = $table;