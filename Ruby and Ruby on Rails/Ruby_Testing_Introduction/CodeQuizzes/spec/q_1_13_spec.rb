require 'q_1_13'

describe "#to_money" do
  it "converts float to money format" do
    expect(12.991.to_money).to eq '$12.99'
  end

  it "correctly rounds zeros" do
    expect(9.0.to_money).to eq '$9.00'
  end
end
