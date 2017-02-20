<?php

	if (isset($this->z->core->data['form'])) {
		$this->z->core->data['form']->render();
	}
	if (isset($this->z->core->data['table'])) {
		$this->z->tables->renderTable($this->z->core->data['table']);
	}