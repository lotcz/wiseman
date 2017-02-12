<!DOCTYPE html>
<html lang="en">
	<head>
		<meta charset="utf-8">		
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1">
		<!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->

		<meta name="description" content="wiseman">
		<meta name="author" content="Karel Zavadil">

		<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">		
		<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css">
				
		<title>Wiseman - <?=$this->data['page_title'] ?></title>
	</head>

	<body>
		<nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
			<div class="container">
				<!-- grouped for better mobile display -->
				<div class="navbar-header">
					<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
						<span class="sr-only">Toggle navigation</span>
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
						<span class="icon-bar"></span>
					 </button>

					 <a class="navbar-brand" href="<?=$this->url('') ?>"><?=$this->t('Home') ?></a>
							
				</div>
				
				<?php
					if ($this->isAuth()) {
						?>
							<div class="collapse navbar-collapse" id="navbar">		
								<ul class="nav navbar-nav navbar-left">
									<?php							
										$this->renderMenuLink('admin/quotes', 'Quotes');
										$this->renderMenuLink('admin/contexts', 'Contexts');					
										$this->renderMenuLink('admin/sources', 'Sources');							
										$this->renderMenuLink('admin/sources', 'Authors');
									?>
									<li class="dropdown">
										<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><?=$this->t('More...') ?><span class="caret"></span></a>
										<ul class="dropdown-menu">								
											<li class="dropdown-header"><?=$this->t('Administrators') ?></li>
											<?php
												$this->renderMenuLink('admin/users', 'Administrators');
												$this->renderMenuLink('admin/roles', 'Administrator roles');
												
											?>
											<li role="separator" class="divider"></li>
											<li class="dropdown-header"><?=$this->t('Advanced') ?></li>
											<?php
												$this->renderMenuLink('admin/languages', 'Languages');
												$this->renderMenuLink('admin/ip-failed-attempts', 'Failed login attempts');
												$this->renderMenuLink('admin/jobs', 'Jobs');
												$this->renderMenuLink('admin/phpinfo', 'PHP Info');
											?>
									  </ul>
									</li>
								</ul>
								<ul class="nav navbar-nav navbar-right">
									<li><a href="<?=$this->url('admin/default/user/' . $this->getUser()->val('user_id')) ?>"><?=$this->getUser()->val('user_email') ?></a></li>
									<li><a href="<?=$this->url('admin/logout') ?>"><?=$this->t('Log Out') ?></a></li>
								</ul>
							</div>
						<?php
					}
				?>
			</div>
		</nav>
	
		<?php
			$this->renderMainView();
		?>		
		<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

	</body>
</html>