module MatchesHelper

  def modified_notice
    if notice == nil
      "".html_safe
    else
      add_edit_action_notice
    end
  end

  def add_edit_action_notice
  "<div class='alert alert-success alert-dismissable'>
    <button type='button' class='close' data-dismiss='alert' aria-hidden='true'>x</button>
    <strong> #{notice} </strong>
  </div>".html_safe
  end

end
