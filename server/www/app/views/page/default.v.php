<h1>Hello, I am Wiseman</h1>

<p>
	<?php
		if ($this->dataExists('random_quote')) {
			?>	
				Quote of the day is:
				<br/>
				<strong>
					<?=$this->getData('random_quote')->val('quote_text'); ?>
				</strong>
				<br/>
				by <strong><?=$this->getData('random_quote')->val('author_name'); ?></strong>
				from <strong><?=$this->getData('random_quote')->val('source_name'); ?></strong>
			<?php
		} else {
			echo $this->t('No quotes in wiseman database.');
		}
	?>
</p>

<p>Go to 
	<?php
		$this->renderLink('admin', 'Admin');
	?>
	area.
</p>